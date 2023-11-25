<br />
<div align="center">
  <h3 align="center">TestTaskLabInvent</h3>

  <p align="center">
    Тестовое задание для LabInvent
    <br />
  </p>
</div>



<details>
  <summary>Оглавление</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>



## About The Project

Обработка XML файлов с использованием микросервисного подхода. (C#
RabbitMQ, SQLite)

## Getting Started

Описание шагов для запуска программы 

### Prerequisites

Действия для установки `docker-compose` на Ubuntu
* Требуется для установки и выполнения последующих шагов
  ```sh
  sudo apt-get install curl
  sudo apt-get install gnupg
  sudo apt-get install ca-certificates
  sudo apt-get install lsb-release
  ```
* Загрузка файла `gpg` для установки `docker`
  ```sh
  sudo mkdir -p /etc/apt/keyrings
  curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
  ```
* Добавление пакетов `docker` и `docker-compose` в Ubuntu
  ```
  echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-pluginsudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-pluginlinux/ubuntu   $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
  ```
* Уставнока `docker` и `docker-compose`
  ```
  sudo apt-get update
  sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin
  ```
* Проверка успешности уставноки `docker`
  ```
  sudo docker run hello-world
  ```

### Installation

* Клонирование репозитория
   ```sh
   git clone https://github.com/code-lime/TestTaskLabInvent
   ```
* Настройка параметров запуска
   ```sh
   nano .env
   ```
* Запуск `docker-compose`
   ```sh
   docker-compose up
   ```

## Usage

Для обработки `.xml` файлов требуется их разместить в папке `./ext/files`, которая была создана после запуска `docker-compose`.

В папке `./ext/sqlite` находится файл базы данных в который происходит сохранение измененых данных из `.xml` файлов отправленных через `RabbitMQ`