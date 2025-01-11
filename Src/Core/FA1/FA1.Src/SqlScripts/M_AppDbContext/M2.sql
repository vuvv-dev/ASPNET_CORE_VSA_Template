BEGIN TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

ALTER TABLE todo.user_token ADD "Discriminator" character varying(34) NOT NULL DEFAULT '';

ALTER TABLE todo.user_token ADD expired_at TIMESTAMP WITH TIME ZONE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250111081924_M2', '9.0.0');

COMMIT;

