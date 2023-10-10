# Autenticación de Dos Factores (MFA) con Correo Electrónico y Códigos QR

Este proyecto implementa un sistema de autenticación de dos factores que utiliza correo electrónico y códigos QR para garantizar la seguridad de los usuarios al acceder a nuestra página web.

Aqui dejamos un video donmde explicamos como aplicamos este ejercicio en .NET y en una estructura de 4 capas.
https://youtu.be/benoRz7F2A0

## Tabla de Contenidos

- [Introducción](#introducción)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Configuración del Proyecto](#configuración-del-proyecto)
- [Endpoints API](#endpoints-api)
- [Ejecución del Proyecto](#ejecución-del-proyecto)
- [Beneficios de la Implementación](#beneficios-de-la-implementación)
- [Agradecimientos](#agradecimientos)

## Introducción

La autenticación de dos factores, más conocida en siglas como MFA (Multi-Factor Authentication), es una capa adicional de seguridad que requiere que los usuarios proporcionen dos formas diferentes de identificación antes de acceder a una aplicación o servicio. En este proyecto, implementamos la autenticación de dos factores utilizando correo electrónico y códigos QR generados en una aplicación móvil.

## Tecnologías Utilizadas

- C#
- ASP.NET Core
- Entity Framework Core
- Bibliotecas de Autenticación (TwoFactorAuth.Net, TwoFactorAuth.Net.QRCoder, System.IdentityModel.Tokens.Jw)
- Aplicaciones de Autenticación Móvil (Google Authenticator)
- MySQL (para la base de datos)

## Configuración del Proyecto

Antes de ejecutar el proyecto, asegúrate de configurar adecuadamente las siguientes variables de entorno o archivos de configuración:

- **appsettings.json**: `server=localhost;user=root;password="AQUÍ";database=AuthTwoStep`
- **appsettings.Development.json**: `server=localhost;user=root;password="AQUÍ";database=AuthTwoStep`

## Endpoints API

El proyecto ofrece los siguientes endpoints de la API:

### Login

Permite a los usuarios iniciar sesión y genera un código QR que se enviará por correo electrónico. Los usuarios deben escanear este código para obtener un código de acceso a la página web.

- **Método HTTP**: `POST`
- **Ruta**: `/api/User/Login`
- Parámetros de entrada: `Username` (nombre de usuario) y `Password` (contraseña)
- Respuesta exitosa: Código HTTP 200 OK, con un mensaje que indica que el QR se ha enviado al correo electrónico.

### VerifyCode

Permite a los usuarios verificar el código QR escaneado y autenticarse en la página web. Deben proporcionar el código generado por la aplicación móvil.

- **Método HTTP**: `POST`
- **Ruta**: `/api/User/VerifyCode`
- Parámetros de entrada: `Username` (nombre de usuario) y `Code` (código generado por la aplicación móvil)
- Respuesta exitosa: Código HTTP 200 OK, si la autenticación es exitosa. Código HTTP 401 Unauthorized en caso contrario.

### Register

Permite a los usuarios registrarse en la aplicación.

- **Método HTTP**: `POST`
- **Ruta**: `/api/User/Register`
- Parámetros de entrada: Datos de registro, como `Username`, `Email`, y `Password`
- Respuesta exitosa: Código HTTP 201 Created, con los detalles del usuario registrado.

## Ejecución del Proyecto

1. Clona este repositorio o descarga directamente los archivos del proyecto.

2. Configura las variables de entorno y archivos de configuración como se mencionó anteriormente en `appsettings.json` y `appsettings.Development.json`.

3. Para ejecutar la aplicación, abre una terminal utilizando Visual Studio y ejecuta el siguiente comando: `dotnet run --project .\ApiAuth\`.

4. Para realizar consultas y acceder a la API, utiliza los endpoints mencionados anteriormente dentro de alguna extensión o aplicación externa como Insomnia.

5. Finalmente, para probar nuestra interfaz Frontend, abre con Live Server y regístrate.

## Beneficios de la Implementación

La implementación de la autenticación de dos factores en ASP.NET Core fortalecerá la seguridad de la aplicación, protegiendo las cuentas de usuario contra el acceso no autorizado. Esto ayudará a garantizar la integridad de los datos y la confidencialidad de la información sensible, lo que a su vez aumentará la confianza de los usuarios en la aplicación.

## Agradecimientos

¡Gracias por usar este proyecto de autenticación de dos factores! Si tienes alguna pregunta o sugerencia, no dudes en ponerte en contacto con el equipo de desarrollo. Somos Edward Carvajal Hithel y Daniela López Danilop109.
