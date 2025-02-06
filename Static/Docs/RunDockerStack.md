# ⚙️ Customize docker stack

**CAUTIONS**: Please make sure you have docker running directly in your machine or through vm first, and test the connection from your docker client on your machine to the docker server.

| ⚡ TL;DR (quick version)                                                                                                                                                                                         |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| There are a lot of thing to customize but if you don't care much, only change the **`HOST_IP`** var in `.env` file in `AppInfrastructure` folder (docker stack), leave the other unchanged and **RUN THE STACK** |

This folder contains necessary files to run a docker stack using docker compose. We have:

- `docker-compose.yml`: all config of docker stack like docker images, network, volume is in here.

- `.env`: Some configurations for docker stack,
  mainly contain sensitive informations like key, password, etc. So you don't want to commit this to remote repo (Github, Bitbucket, etc), but since this is just a template, I can commit it freely.

- `.dockerignore`: ignore some files or folders when building the docker image (e.g., .vs, .vscode), make the image smaller

- `Customs`: This folder contain custom images. Not everytime you would use a default image made by others, you gotta do some custom, like adding `acl file` to `redis`, adding config file to `nginx` and additional other things.

### How to run

**1. Change directory to AppInfrastructure by the following command:**

```bash
cd {ROOT_PATH}/AppInfrastructure
```

In this case, `{ROOT_PATH}` is the root path of the project

For example, you clone the project into `D:\Project\ASPNET_CORE_VSA_Template`, then you can run:

```bash
cd D:\Project\ASPNET_CORE_VSA_Template\AppInfrastructure
```

**2. Find and change the `HOST_IP` in `.env` file to your docker server IP**

For example:

Currently, in `.env` there is a section at the top expressing docker server IP:

```bash
# ======================
# GLOBAL
# ======================
HOST_IP=192.168.56.104
```

But your docker server IP is `192.168.1.10`, so you must change it to:

```bash
# ======================
# GLOBAL
# ======================
HOST_IP=192.168.1.10
```

**3. Run the docker stack by the following command:**

```bash
docker compose up -d --build
```

### How to stop

**1. Change directory to AppInfrastructure by the following command:**

```bash
cd {ROOT_PATH}/AppInfrastructure
```

In this case, `{ROOT_PATH}` is the root path of the project

**For example**, you clone the project into `D:\Project\ASPNET_CORE_VSA_Template`, then you can run:

```bash
cd D:\Project\ASPNET_CORE_VSA_Template\AppInfrastructure
```

**2. Stop the docker stack by the following command:**

```bash
docker compose down
```

## Again: There are more to customize but for me, this is enough.
