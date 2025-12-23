-- =================================================================
-- DDL-СКРИПТ ДЛЯ СОЗДАНИЯ СТРУКТУРЫ БАЗЫ ДАННЫХ
-- "ИНФОРМАЦИОННАЯ СИСТЕМА ДЛЯ УЧЕТА ПАССАЖИРОПЕРЕВОЗОК"
-- СУБД: PostgreSQL
-- =================================================================

-- Удаление таблиц, если они уже существуют, для чистого запуска скрипта
DROP TABLE IF EXISTS Tickets;
DROP TABLE IF EXISTS Trips_Drivers;
DROP TABLE IF EXISTS Routes_Stops;
DROP TABLE IF EXISTS Trips;
DROP TABLE IF EXISTS Passengers;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Authorizations;
DROP TABLE IF EXISTS Roles;
DROP TABLE IF EXISTS Drivers;
DROP TABLE IF EXISTS Buses;
DROP TABLE IF EXISTS Stops;
DROP TABLE IF EXISTS Routes;

-- =================================================================
-- 1. СПРАВОЧНЫЕ ТАБЛИЦЫ (не зависят от других)
-- =================================================================

-- Таблица маршрутов
CREATE TABLE Routes (
    route_id SERIAL PRIMARY KEY,
    route_number VARCHAR(20) NOT NULL UNIQUE,
    departure_point VARCHAR(100) NOT NULL,
    destination_point VARCHAR(100) NOT NULL,
    duration_minutes INT NOT NULL CHECK (duration_minutes > 0)
);

-- Таблица остановок
CREATE TABLE Stops (
    stop_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

-- Таблица автобусов
CREATE TABLE Buses (
    bus_id SERIAL PRIMARY KEY,
    license_plate VARCHAR(15) NOT NULL UNIQUE,
    model VARCHAR(100) NOT NULL,
    seat_capacity INT NOT NULL CHECK (seat_capacity > 0),
    bus_image BYTEA
);

-- Таблица водителей
CREATE TABLE Drivers (
    driver_id SERIAL PRIMARY KEY,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50)
);

-- Таблица ролей пользователей
CREATE TABLE Roles (
    role_id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE
);

-- Таблица для данных аутентификации
CREATE TABLE Authorizations (
    login VARCHAR(50) PRIMARY KEY,
    password_hash VARCHAR(255) NOT NULL
);

-- Таблица пассажиров
CREATE TABLE Passengers (
    passenger_id SERIAL PRIMARY KEY,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50),
    passport_number VARCHAR(15) NOT NULL UNIQUE,
    birth_year INT NOT NULL CHECK (birth_year > 1900)
);


-- =================================================================
-- 2. ЗАВИСИМЫЕ И СВЯЗУЮЩИЕ ТАБЛИЦЫ
-- =================================================================

-- Таблица пользователей системы (сотрудников)
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    role_id INT NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(50),
    login VARCHAR(50) NOT NULL UNIQUE,

    CONSTRAINT fk_users_role FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE RESTRICT,
    CONSTRAINT fk_users_login FOREIGN KEY (login) REFERENCES Authorizations(login) ON DELETE RESTRICT
);

-- Ассоциативная таблица для связи "Маршруты - Остановки" (М:N)
CREATE TABLE Routes_Stops (
    route_id INT NOT NULL,
    stop_id INT NOT NULL,
    stop_order INT NOT NULL CHECK (stop_order > 0),
    arrival_time_from_start INT NOT NULL CHECK (arrival_time_from_start >= 0),

    PRIMARY KEY (route_id, stop_id),
    CONSTRAINT fk_routestops_route FOREIGN KEY (route_id) REFERENCES Routes(route_id) ON DELETE RESTRICT,
    CONSTRAINT fk_routestops_stop FOREIGN KEY (stop_id) REFERENCES Stops(stop_id) ON DELETE RESTRICT
);

-- Таблица конкретных рейсов
CREATE TABLE Trips (
    trip_id SERIAL PRIMARY KEY,
    route_id INT NOT NULL,
    bus_id INT NOT NULL,
    created_by_user_id INT NOT NULL,
    departure_datetime TIMESTAMPTZ NOT NULL,
    arrival_datetime TIMESTAMPTZ NOT NULL,

    CONSTRAINT fk_trips_route FOREIGN KEY (route_id) REFERENCES Routes(route_id) ON DELETE RESTRICT,
    CONSTRAINT fk_trips_bus FOREIGN KEY (bus_id) REFERENCES Buses(bus_id) ON DELETE RESTRICT,
    CONSTRAINT fk_trips_user FOREIGN KEY (created_by_user_id) REFERENCES Users(user_id) ON DELETE RESTRICT,
    CHECK (arrival_datetime > departure_datetime) -- Проверка логичности дат
);

-- Ассоциативная таблица для связи "Рейсы - Водители" (М:N)
CREATE TABLE Trips_Drivers (
    trip_id INT NOT NULL,
    driver_id INT NOT NULL,

    PRIMARY KEY (trip_id, driver_id),
    CONSTRAINT fk_tripdrivers_trip FOREIGN KEY (trip_id) REFERENCES Trips(trip_id) ON DELETE RESTRICT,
    CONSTRAINT fk_tripdrivers_driver FOREIGN KEY (driver_id) REFERENCES Drivers(driver_id) ON DELETE RESTRICT
);

-- Таблица проданных билетов
CREATE TABLE Tickets (
    ticket_id SERIAL PRIMARY KEY,
    trip_id INT NOT NULL,
    passenger_id INT NOT NULL,
    destination_stop_id INT NOT NULL,
    sold_by_user_id INT NOT NULL,
    seat_number INT NOT NULL CHECK (seat_number > 0),
    sale_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    cost NUMERIC(10, 2) NOT NULL CHECK (cost >= 0),

    CONSTRAINT uq_trip_seat UNIQUE (trip_id, seat_number), -- Ключевое ограничение!
    CONSTRAINT fk_tickets_trip FOREIGN KEY (trip_id) REFERENCES Trips(trip_id) ON DELETE RESTRICT,
    CONSTRAINT fk_tickets_passenger FOREIGN KEY (passenger_id) REFERENCES Passengers(passenger_id) ON DELETE RESTRICT,
    CONSTRAINT fk_tickets_stop FOREIGN KEY (destination_stop_id) REFERENCES Stops(stop_id) ON DELETE RESTRICT,
    CONSTRAINT fk_tickets_user FOREIGN KEY (sold_by_user_id) REFERENCES Users(user_id) ON DELETE RESTRICT
);

-- =================================================================
-- СОЗДАНИЕ ИНДЕКСОВ ДЛЯ УСКОРЕНИЯ ЗАПРОСОВ
-- =================================================================

CREATE INDEX idx_tickets_trip_id ON Tickets(trip_id);
CREATE INDEX idx_tickets_passenger_id ON Tickets(passenger_id);
CREATE INDEX idx_trips_route_id ON Trips(route_id);
CREATE INDEX idx_trips_bus_id ON Trips(bus_id);
CREATE INDEX idx_users_role_id ON Users(role_id);

-- =================================================================
-- КОНЕЦ СКРИПТА
-- =================================================================