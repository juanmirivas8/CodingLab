# 🔐 Configuración de Secrets y Variables de Entorno

Este documento explica cómo configurar correctamente los secrets y variables de entorno en el proyecto DotNetLab.

## 📋 Tabla de Contenidos
- [Desarrollo Local](#desarrollo-local)
- [Docker](#docker)
- [Seguridad](#seguridad)

---

## 🖥️ Desarrollo Local

### Usar User Secrets (Recomendado)

**User Secrets** es una característica de .NET que almacena información sensible FUERA del código fuente, en tu perfil de usuario de Windows.

#### Configuración para MinimalAPI.Host

##### Paso 1: Inicializar User Secrets

Abre una terminal en el directorio del proyecto `DotNetLab.MinimalAPI.Host` y ejecuta:

```cmd
dotnet user-secrets init
```

##### Paso 2: Agregar tu Connection String

```cmd
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1,1433;Database=DotNetLab.Database;User Id=sa;Password=TU_PASSWORD_AQUI;MultipleActiveResultSets=true;TrustServerCertificate=true"
```

##### Paso 3: Ver tus secrets (opcional)

```cmd
dotnet user-secrets list
```

##### Paso 4: Eliminar un secret (opcional)

```cmd
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

##### Paso 5: Limpiar todos los secrets (opcional)

```cmd
dotnet user-secrets clear
```

#### Configuración para NotificationFunction (Azure Function)

##### Paso 1: Navegar al directorio del proyecto

```cmd
cd C:\Users\Juan Rivas Velasco\Desktop\DotNetLab\DotNetLab.NotificationFunction
```

##### Paso 2: Agregar RabbitMQ Connection String

```cmd
dotnet user-secrets set "RabbitMQConnection" "amqp://guest:guest@localhost:5672"
```

##### Paso 3: Ver tus secrets (opcional)

```cmd
dotnet user-secrets list
```

**Nota**: Para Azure Functions, los secrets se configuran de manera plana (sin jerarquía de objetos), directamente como aparecen en `local.settings.json` → `Values`.

### ¿Dónde se almacenan?

Los User Secrets se guardan en:

**MinimalAPI.Host:**
```
%APPDATA%\Microsoft\UserSecrets\dotnetlab-api-secrets-12345\secrets.json
```

**NotificationFunction:**
```
%APPDATA%\Microsoft\UserSecrets\dotnetlab-function-secrets-67890\secrets.json
```

**Este archivo NUNCA se sube a Git** porque está fuera del proyecto.

---

## 🐳 Docker

### Configurar Variables de Entorno para Docker Compose

#### Paso 1: Copiar el archivo de ejemplo

En el directorio `DeploymentFiles`, copia el archivo `.env.example` a `.env`:

```cmd
cd DeploymentFiles
copy .env.example .env
```

#### Paso 2: Editar el archivo .env

Abre el archivo `.env` y reemplaza los valores PLACEHOLDER con tus credenciales reales:

```env
# SQL Server Configuration
SA_PASSWORD=TuPasswordSeguro@123
MSSQL_PID=Developer

# RabbitMQ Configuration
RABBITMQ_DEFAULT_USER=guest
RABBITMQ_DEFAULT_PASS=guest

# Application Configuration
ASPNETCORE_ENVIRONMENT=Docker
ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=DotNetLab.Database;User Id=sa;Password=TuPasswordSeguro@123;MultipleActiveResultSets=true;TrustServerCertificate=true
```

#### Paso 3: Levantar los contenedores

```cmd
docker-compose up -d
```

Docker Compose automáticamente leerá las variables del archivo `.env` y las inyectará en los contenedores.

### ⚠️ IMPORTANTE

El archivo `.env` está en `.gitignore` y **NUNCA debe subirse a Git**.

---

## 🔒 Seguridad

### ✅ Buenas Prácticas

1. **NUNCA** subas archivos `.env` a Git
2. **NUNCA** pongas passwords en `appsettings.json`
3. Usa **User Secrets** para desarrollo local
4. Usa **Variables de Entorno** para Docker/Producción
5. Comparte el archivo `.env.example` con tu equipo (sin valores reales)
6. Cambia las passwords regularmente
7. Usa passwords fuertes (mínimo 12 caracteres, mayúsculas, minúsculas, números, símbolos)

### ❌ Evitar

- ❌ Hardcodear passwords en el código
- ❌ Compartir archivos `.env` por email/chat
- ❌ Usar la misma password en desarrollo y producción
- ❌ Commits con información sensible

### 🛡️ Si accidentalmente subes un secret a Git

1. **Cambia inmediatamente** la password/secret comprometido
2. Elimina el commit del historial de Git (usa `git filter-branch` o BFG Repo-Cleaner)
3. Fuerza un push: `git push --force`
4. Notifica a tu equipo

---

## 🆘 Troubleshooting

### Error: "Connection string not found"

**Solución**: Asegúrate de haber configurado los User Secrets o variables de entorno.

### Error: "Login failed for user 'sa'"

**Solución**: Verifica que la password en User Secrets o `.env` coincida con la del servidor SQL.

### Los cambios en .env no se reflejan

**Solución**: Reinicia los contenedores:
```cmd
docker-compose down
docker-compose up -d
```

### Ver si User Secrets está configurado

```cmd
dotnet user-secrets list
```

Si está vacío, necesitas agregar tus secrets.

---

## 📚 Recursos Adicionales

- [Documentación oficial de User Secrets](https://learn.microsoft.com/es-es/aspnet/core/security/app-secrets)
- [Configuración en ASP.NET Core](https://learn.microsoft.com/es-es/aspnet/core/fundamentals/configuration/)
- [Docker Compose Environment Variables](https://docs.docker.com/compose/environment-variables/)

---

## 🤝 Contribuir

Si encuentras formas de mejorar la seguridad de este proyecto, por favor:
1. Abre un issue
2. Propón una solución
3. Mantén las mejores prácticas de seguridad
