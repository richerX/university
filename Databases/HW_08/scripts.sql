drop table if exists JOBS;
drop table if exists JOB_HISTORY;
drop table if exists EMPLOYEES;

CREATE TABLE JOBS (
    job_id text primary key,
    job_title text,
    min_salary int,
    max_salary int
);

CREATE TABLE JOB_HISTORY (
    employee_id int,
    start_date date,
    end_date date,
    job_id text
);

CREATE TABLE EMPLOYEES (
    employee_id int primary key,
    hire_date date,
    job_id text,
    salary int
);


CREATE PROCEDURE new_job(id text, title text, minimum integer)
LANGUAGE sql
AS $$
    INSERT INTO jobs VALUES (id, title, minimum, minimum * 2);
$$;

CALL NEW_JOB('SY_ANAL', 'System Analyst', 6000);


CREATE OR REPLACE PROCEDURE add_job_hist(new_employee_id int, new_job_id text)
LANGUAGE sql
AS $$
    INSERT INTO job_history
    SELECT employee_id, hire_date, CURRENT_DATE, job_id
    FROM employees
    WHERE employee_id = new_employee_id;

    UPDATE employees
    SET hire_date = CURRENT_DATE,
    job_id = new_job_id,
    salary = (SELECT min_salary + 500 FROM jobs WHERE job_id = new_job_id)
    WHERE employee_id = new_employee_id;
$$;

SET session_replication_role = replica;
CALL add_job_hist(106, 'SY_ANAL');
SET session_replication_role = DEFAULT;


CREATE OR REPLACE PROCEDURE upd_jobsal(id text, new_min_salary int, new_max_salary int)
LANGUAGE sql
AS $$
    UPDATE jobs
    SET min_salary = new_min_salary,
        max_salary = new_max_salary
    WHERE job_id = id;
$$;


CREATE OR REPLACE FUNCTION get_years_service(id int)
RETURNS float
LANGUAGE plpgsql
AS $$
DECLARE
    answer int = 0;
    cur_row job_history%rowtype;
BEGIN
    FOR cur_row in SELECT * FROM job_history LOOP
        IF cur_row.employee_id = id THEN
            IF cur_row.end_date IS NULL THEN
                answer = answer + CURRENT_DATE - cur_row.start_date;
            ELSE
                answer = answer + cur_row.end_date - cur_row.start_date;
            end if;
        END IF;
    END LOOP;

    RETURN (answer::float / 365.0);
END;
$$;

SELECT get_years_service(106) as years;


CREATE OR REPLACE FUNCTION get_job_count(id int)
RETURNS int
LANGUAGE plpgsql
AS $$
DECLARE
    answer int = 0;
BEGIN
    SELECT count(DISTINCT job_id)
    INTO answer
    FROM job_history
    WHERE employee_id = id;

    RETURN answer;
END;
$$;

SELECT get_job_count(176);


CREATE OR REPLACE FUNCTION check_sal_range_func()
RETURNS trigger
LANGUAGE plpgsql
AS $$
BEGIN
    IF EXISTS(SELECT * FROM employees where employees.job_id = NEW.job_id and salary < NEW.min_salary) THEN
        RAISE EXCEPTION 'New minimum salary is not correct';
    ELSEIF EXISTS(SELECT * FROM employees where employees.job_id = NEW.job_id and salary > NEW.max_salary) THEN
        RAISE EXCEPTION 'New maximum salary is not correct';
    END IF;

    RETURN NEW;
END;
$$;

DROP TRIGGER IF EXISTS check_sal_range ON jobs;

CREATE TRIGGER check_sal_range
BEFORE UPDATE
ON jobs
FOR EACH ROW
EXECUTE PROCEDURE check_sal_range_func();

UPDATE jobs SET min_salary = 7000, max_salary = 12000 WHERE job_id = 'A'; -- ok
UPDATE jobs SET min_salary = 10000, max_salary = 15000 WHERE job_id = 'A'; -- error
