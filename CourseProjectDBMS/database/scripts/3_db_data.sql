-- Файл: 03_db_data.sql
-- Описание: Наполнение БД тестовыми данными

-- 1. Роли
INSERT INTO Roles (role_name) VALUES 
('Администратор'), 
('Кассир'), 
('Менеджер');

-- 2. Авторизация (пароли пока открытые для теста)
INSERT INTO Authorizations (login, password_hash) VALUES 
('admin', 'admin'),
('ivanova', '12345'),
('petrov', 'qwerty');

-- 3. Сотрудники
INSERT INTO Users (role_id, last_name, first_name, middle_name, login) VALUES 
(1, 'Зонин', 'М.', 'Д.', 'admin'),
(2, 'Иванова', 'Мария', 'Ивановна', 'ivanova'),
(3, 'Петров', 'Сергей', 'Александрович', 'petrov');

-- 4. Остановки
INSERT INTO Stops (name) VALUES 
('Муром (Автовокзал)'), 
('Владимир'), 
('Москва'), 
('Нижний Новгород'), 
('Рязань'), 
('Касимов'), 
('Навашино'), 
('Гороховец'), 
('Покров'), 
('Меленки');

-- 5. Автобусы
INSERT INTO Buses (license_plate, model, seat_capacity) VALUES
('А777АА33', 'Mercedes-Benz Sprinter', 20),
('В123ОР33', 'Higer KLQ', 45),
('К999ММ33', 'ПАЗ Vector Next', 25),
('Е001КХ77', 'Neoplan Cityliner', 50);

-- 6. Водители
INSERT INTO Drivers (last_name, first_name, middle_name) VALUES
('Сидоров', 'Алексей', 'Петрович'),
('Кузнецов', 'Дмитрий', 'Иванович'),
('Смирнов', 'Олег', 'Сергеевич'),
('Васильев', 'Иван', 'Николаевич');

-- 7. Маршруты
INSERT INTO Routes (route_number, departure_point, destination_point, duration_minutes) VALUES
('505', 'Муром', 'Москва', 360),
('101', 'Муром', 'Владимир', 180),
('202', 'Муром', 'Нижний Новгород', 240);

-- 8. Связи маршрутов и остановок
INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start) VALUES
(1, 1, 1, 0), (1, 2, 2, 120), (1, 9, 3, 240), (1, 3, 4, 360), -- Муром->Москва
(2, 1, 1, 0), (2, 2, 2, 180),                                -- Муром->Владимир
(3, 1, 1, 0), (3, 7, 2, 40), (3, 8, 3, 120), (3, 4, 4, 240); -- Муром->НН

-- 9. Рейсы (даты выставлены на будущее для удобства тестирования)
INSERT INTO Trips (route_id, bus_id, created_by_user_id, departure_datetime, arrival_datetime) VALUES
(1, 4, 3, NOW() + INTERVAL '1 day', NOW() + INTERVAL '1 day 6 hours'),
(1, 2, 3, NOW() + INTERVAL '2 day', NOW() + INTERVAL '2 day 6 hours'),
(2, 1, 3, NOW() + INTERVAL '5 hours', NOW() + INTERVAL '8 hours'),
(3, 3, 3, NOW() + INTERVAL '1 day 2 hours', NOW() + INTERVAL '1 day 6 hours');

-- 10. Водители на рейсе
INSERT INTO Trips_Drivers (trip_id, driver_id) VALUES
(1, 1), (1, 2), -- На Москву два водителя
(2, 3), (2, 4), -- На вторую Москву тоже два
(3, 1),
(4, 2);

-- 11. Пассажиры
INSERT INTO Passengers (last_name, first_name, middle_name, passport_number, birth_year) VALUES
('Волкова', 'Елена', 'Сергеевна', '1111000001', 1995),
('Зайцев', 'Игорь', 'Владимирович', '1111000002', 1988),
('Соловьев', 'Максим', 'Андреевич', '1111000003', 2001),
('Орлова', 'Ольга', 'Дмитриевна', '1111000004', 1975),
('Козлов', 'Павел', 'Игоревич', '1111000005', 1990);

-- 12. Билеты (Продажа тестовых билетов)
INSERT INTO Tickets (trip_id, passenger_id, destination_stop_id, sold_by_user_id, seat_number, cost) VALUES
(1, 1, 3, 2, 1, 1500.00), -- На рейс 1, место 1
(1, 2, 3, 2, 2, 1500.00), -- На рейс 1, место 2
(3, 3, 2, 2, 5, 450.00);  -- На рейс 3, место 5