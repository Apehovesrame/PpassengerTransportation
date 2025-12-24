-- Файл: 02_db_logic.sql
-- Описание: Хранимые процедуры и триггеры

-- Функция проверки вместимости автобуса
CREATE OR REPLACE FUNCTION check_bus_capacity()
RETURNS TRIGGER AS $$
DECLARE
    current_capacity INT;
    sold_tickets INT;
BEGIN
    -- 1. Получаем вместимость автобуса, назначенного на рейс
    SELECT b.seat_capacity INTO current_capacity
    FROM Trips t
    JOIN Buses b ON t.bus_id = b.bus_id
    WHERE t.trip_id = NEW.trip_id;

    -- 2. Считаем количество уже проданных билетов на этот рейс
    SELECT COUNT(*) INTO sold_tickets
    FROM Tickets
    WHERE trip_id = NEW.trip_id;

    -- 3. Проверка: есть ли вообще свободные места
    IF (sold_tickets >= current_capacity) THEN
        RAISE EXCEPTION 'Ошибка: Нет свободных мест. Вместимость: %, Продано: %', current_capacity, sold_tickets;
    END IF;

    -- 4. Проверка: существует ли такое кресло физически
    IF (NEW.seat_number > current_capacity) THEN
         RAISE EXCEPTION 'Ошибка: Места с номером % не существует в этом автобусе (Вместимость: %)', NEW.seat_number, current_capacity;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Создание триггера, который запускает функцию ПЕРЕД вставкой билета
DROP TRIGGER IF EXISTS trg_check_capacity ON Tickets;
CREATE TRIGGER trg_check_capacity
BEFORE INSERT ON Tickets
FOR EACH ROW
EXECUTE FUNCTION check_bus_capacity();