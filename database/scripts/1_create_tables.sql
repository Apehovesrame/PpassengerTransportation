-- 1. Таблица авторизации (вынесена отдельно по схеме)
-- Хранит логин и хеш пароля.
CREATE TABLE Authorizations (
    login VARCHAR(50) PRIMARY KEY,
    password_hash VARCHAR(255) NOT NULL
);

-- 2. Роли пользователей (Админ, Кассир и т.д.)
CREATE TABLE Roles (
    role_id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE
);

-- 3. Сотрудники (Users)
-- Ссылаются на авторизацию (login) и роль.
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    role_id INT NOT NULL REFERENCES Roles(role_id),
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50),
    login VARCHAR(50) NOT NULL UNIQUE REFERENCES Authorizations(login)
);

-- 4. Автобусы
-- bus_image имеет тип BYTEA для хранения картинки прямо в БД (по ТЗ).
CREATE TABLE Buses (
    bus_id SERIAL PRIMARY KEY,
    license_plate VARCHAR(15) NOT NULL UNIQUE,
    model VARCHAR(100) NOT NULL,
    seat_capacity INT NOT NULL CHECK (seat_capacity > 0),
    bus_image BYTEA 
);

-- 5. Водители
CREATE TABLE Drivers (
    driver_id SERIAL PRIMARY KEY,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50)
);

-- 6. Остановки (географические пункты)
CREATE TABLE Stops (
    stop_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

-- 7. Маршруты (абстрактное описание пути)
CREATE TABLE Routes (
    route_id SERIAL PRIMARY KEY,
    route_number VARCHAR(20) NOT NULL UNIQUE,
    departure_point VARCHAR(100) NOT NULL,
    destination_point VARCHAR(100) NOT NULL,
    duration_minutes INT NOT NULL CHECK (duration_minutes > 0)
);

-- 8. Остановки на маршруте (Связь "Многие-ко-многим")
-- Составной первичный ключ гарантирует, что одна остановка не дублируется в маршруте.
CREATE TABLE Routes_Stops (
    route_id INT NOT NULL REFERENCES Routes(route_id) ON DELETE CASCADE,
    stop_id INT NOT NULL REFERENCES Stops(stop_id) ON DELETE RESTRICT,
    stop_order INT NOT NULL CHECK (stop_order > 0), -- Порядковый номер остановки
    arrival_time_from_start INT NOT NULL CHECK (arrival_time_from_start >= 0), -- Время прибытия от начала
    PRIMARY KEY (route_id, stop_id)
);

-- 9. Рейсы (Конкретная поездка)
-- Используем TIMESTAMPTZ для корректной работы с часовыми поясами (по ТЗ).
CREATE TABLE Trips (
    trip_id SERIAL PRIMARY KEY,
    route_id INT NOT NULL REFERENCES Routes(route_id),
    bus_id INT NOT NULL REFERENCES Buses(bus_id),
    created_by_user_id INT NOT NULL REFERENCES Users(user_id),
    departure_datetime TIMESTAMPTZ NOT NULL,
    arrival_datetime TIMESTAMPTZ NOT NULL,
    CHECK (arrival_datetime > departure_datetime) -- Защита: прибытие не может быть раньше отправления
);

-- 10. Водители на рейсе (Связь "Многие-ко-многим")
-- Позволяет назначать 2 водителей на дальние рейсы.
CREATE TABLE Trips_Drivers (
    trip_id INT NOT NULL REFERENCES Trips(trip_id) ON DELETE CASCADE,
    driver_id INT NOT NULL REFERENCES Drivers(driver_id),
    PRIMARY KEY (trip_id, driver_id)
);

-- 11. Пассажиры
CREATE TABLE Passengers (
    passenger_id SERIAL PRIMARY KEY,
    last_name VARCHAR(20) NOT NULL,
    first_name VARCHAR(20) NOT NULL,
    middle_name VARCHAR(20),
    passport_number VARCHAR(15) NOT NULL UNIQUE,
    birth_year INT NOT NULL CHECK (birth_year > 1900)
);

-- 12. Билеты
-- Связывает рейс, пассажира и кассира.
-- destination_stop_id нужен, чтобы знать, до куда едет человек (влияет на цену и место).
CREATE TABLE Tickets (
    ticket_id SERIAL PRIMARY KEY,
    trip_id INT NOT NULL REFERENCES Trips(trip_id),
    passenger_id INT NOT NULL REFERENCES Passengers(passenger_id),
    destination_stop_id INT NOT NULL REFERENCES Stops(stop_id),
    sold_by_user_id INT NOT NULL REFERENCES Users(user_id),
    seat_number INT NOT NULL CHECK (seat_number > 0),
    sale_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    cost NUMERIC(10, 2) NOT NULL CHECK (cost >= 0), -- NUMERIC для денег, чтобы не терять копейки
    UNIQUE (trip_id, seat_number) -- Важно: Защита от двойной продажи одного места
);