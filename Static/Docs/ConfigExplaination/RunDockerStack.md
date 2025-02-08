# Run Docker Stack

This guide explains how to customize and run the Docker stack defined in the `AppInfrastructure` folder. It's crucial to have Docker Desktop (or Docker Engine) running and accessible from your machine before proceeding. Test your Docker connection before starting.

**Quick Summary:** This folder contains the files necessary to run your application using Docker Compose. You can customize these files to match your specific needs.

**Folder Contents:**

- **`docker-compose.yml`:** This file defines the services (containers) that make up your application, including the images to use, network configurations, data volumes, and port mappings. **This is the primary file you'll customize.**

- **`.env`:** This file stores environment variables, especially sensitive information like API keys, passwords, and database credentials. **Do not commit this file to version control (like GitHub).** While it's included in this template for demonstration, in a real project, use a `.env` file locally and don't commit it.

- **`.dockerignore`:** This file lists files and folders that Docker should ignore when building images. This helps keep image sizes smaller and prevents unnecessary files from being included (e.g., `.vs`, `.vscode` folders).

- **`Customs`:** This folder contains custom Docker images. Sometimes, you need to modify existing images (e.g., adding ACL files to Redis or configuration files to Nginx). This folder is where you'd store the Dockerfiles for these custom images.

**Running the Docker Stack:**

1.  **Navigate to the `AppInfrastructure` directory:** Open your terminal or command prompt and navigate to the `AppInfrastructure` folder within your project. For example, if your project is in `D:\Project\ASPNET_CORE_VSA_Template`, the command would be:

    ```bash
    cd D:\Project\ASPNET_CORE_VSA_Template\AppInfrastructure
    ```

2.  **Configure the `.env` file:** Open the `.env` file and set the `HOST_IP` variable to the IP address of your Docker server. This is essential for your application to connect to services running within Docker.

    ```
    # ======================
    # GLOBAL
    # ======================
    HOST_IP=192.168.1.10  # Replace with your Docker server's IP
    ```

3.  **Update connection strings in `appsettings` files:** Open all files starting with `appsettings` (e.g., `appsettings.Development.json`, `appsettings.Production.json`) located in the `Src/Entry` folder. Update any IP addresses in connection strings (like database connection strings) to match your Docker server's IP address (the same one you used in the `.env` file).

    ```json
    "Database": {
        "Main": {
          "ConnectionString": "Server=192.168.1.10; Port=6102; Database=todoappdb; User ID=admin; Password=Admin123@; SSL Mode=Prefer; Pooling=true; Minimum Pool Size=64; Maximum Pool Size=120; Connection Idle Lifetime=300; Connection Lifetime=500",
          // ... other settings
        }
      }
    ```

4.  **Start the Docker stack:** Use the following command to build the images (if necessary) and start the containers:

    ```bash
    docker compose up -d --build
    ```

    The `-d` flag runs the containers in detached mode (in the background). The `--build` flag ensures that Docker rebuilds the images if there are changes to the Dockerfiles.

**Stopping the Docker Stack:**

1.  **Navigate to the `AppInfrastructure` directory:** Just like when starting the stack, navigate to the `AppInfrastructure` folder in your terminal.

2.  **Stop the Docker stack:** Use the following command to stop and remove the containers:

    ```bash
    docker compose down
    ```

## Again: There are more to customize for this file, but for me, this is enough.
