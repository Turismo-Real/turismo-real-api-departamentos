# Turismo Real Departamentos
Servicio REST para consultar información sobre departamentos.  
Actualmente el proyecto se encuentra sin la validación de los datos de entrada.  

## Prerrequisito
Antes de levantar el servicio REST se debe haber levantado completamente la base de datos en Oracle, de lo contrario el servicio no funcionará como corresponde.  
- [Levantar base de datos Turismo Real - Oracle 11g](https://github.com/Turismo-Real/turismo-real-database)
  
## Levantar Servicio
Para poder levantar el servicio localmente, lo primero que se debe asegurar es tener instalado el runtime de .NET Core 3.1 que se puede descargar desde el siguiente enlace *(se recomienda instalar SDK)*: [Runtime y SDK .NET Core 3.1 LTS](https://dotnet.microsoft.com/download).  

### Comprobar instalación de Runtime o SDK  
Abrir una terminal y ejecutar los siguientes comandos, según sea la necesidad. Información extraída de la [documentación de dotnet](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet).
- `dotnet --info`: Obtener información detallada sobre la instalación de .NET en la máquina local.  
- `dotnet --version`: Obtener la versión de .NET SDK.
- `dotnet --list-runtimes`: Obtener lista de los runtimes de .NET instalados en la máquina.
- `dotnet --list-sdks`: Lista los SDKs de .NET instalados en la máquina.

### Compilar y ejecutar servicio desde CLI
Una vez instaladas las herramientas necesarias para la ejecución, el siguiente paso es clonar el repositorio y ubicarse dentro de él, donde se puede encontrar un archivo con la extensión `sln`. En esa ubicación se ejecuta el comando `dotnet build` que comenzará la compilación de la solución.  
  
Una vez compilado, se debe dirigir al directorio `TurismoReal_Departamentos.Api` y dentro ejecutar el comando `dotnet run`.  
Con esto el servicio quedaría corriendo en `http://localhost:5002`.

### Abrir desde Visual Studio
Para abrir el servicio desde visual studio, solamente se debe abrir un proyecto y buscar el archivo que termina con la extensión `sln` y el IDE hace el resto.

---
## Consumir Servicio  
Para consumir el servicio cuando este se encuentra en ejecución, se debe hacer uso de un cliente HTTP como Insomnia o Postman. El servicio cuenta con las siguientes rutas.  
- **GET /api/v1/departamento**
- **GET /api/v1/departamento/{id}**
- **POST /api/v1/departamento**
- **PUT /api/v1/departamento/{id}**
- **DELETE /api/v1/departamento/{id}**  

A continuación se detalla como consumir cada uno de los endpoints del servicio.  

## GET /api/v1/departamento  
Retorna todos los departamentos registrados en la base de datos.
- **URL:** http://localhost:5002/api/v1/departamento
- **Method:** GET
- **Respuesta:** Retorna un arreglo con los usuarios del sistema ordenados por ID. Incluye dirección y lista de instalaciones.  

## GET /api/v1/departamento/{id}
Retorna la información del departamento especificado por ID.  
- **URL:** http://localhost:5002/api/v1/departamento/2
- **Method:** GET
- **Respuesta:** Retorna un objeto con la información del departamento.  
```
# EJEMPLO OBJETO DE SALIDA
{
    "id_departamento": 2,
    "rol": "005-049",
    "dormitorios": 3,
    "banios": 1,
    "descripcion": "Ubicado a los pies de un volcán",
    "superficie": 120,
    "valorDiario": 55000,
    "tipo": "Normal",
    "estado": "Uso",
    "direccion": {
        "region": "Antofagasta",
        "comuna": "Antofagasta",
        "calle": "Calle Europa",
        "numero": "125",
        "depto": "22"
    },
    "instalaciones": [
        "Cable",
        "Internet",
        "Lavandería",
        "Salón de eventos"
    ]
}

```
## POST /api/v1/departamento
Crea un departamento en la base de datos.  
Los atributos `id_epartamento`, `estado` y `region` no son necesarios. El ID del departamento es generado a la hora de ser registrado, el estado por defecto de un departamento recién ingresado es `Cargado` y la región está vinculada con la comuna, por lo que sólo es necesario este último atributo.  
Internamente hace match con las instalaciones existentes, si una instalación ingresa no existe en el sistema, es agregada y luego vinculada al departamento.  
Se debe tener en cuenta los tildes en las instalaciones, ya que basta un tilde para que el sistema las considere distintas.  
- **URL:** http://localhost:5002/api/v1/departamento
- **Method:** POST
- **Respuesta:** Retorna el mismo objeto de entrada, pero incluyendo un mensaje de éxito, el ID generado, estado y región.
```
# EJEMPLO OBJETO DE ENTRADA
{
	"rol": "365-044",
	"dormitorios": 4,
	"banios": 1,
	"descripcion": "Descripcion de prueba X",
	"superficie": 90,
	"valorDiario": 65000,
	"tipo": "norMAL",
	"direccion": {
		"comuna": "San berNARDO",
		"calle": "Calle Santa Maria",
		"numero": "5642",
		"depto": "23A"
	},
    "instalaciones": ["wifi","cable","piscina"]
}

```  

## PUT /api/v1/departamento/{id}
Modifica la información del departamento especificado por ID.  
En el payload, no es necesario enviar el id del departamento ni la región.  
- **URL:** http://localhost:5002/api/v1/departamento/{id}
- **Method:** PUT
- **Respuesta:** Retorna la información del departamento actualizado con un mensaje de éxito.  
```
# EJEMPLO OBJETO DE ENTRADA
{
    "rol": "666-666",
    "dormitorios": 3,
    "banios": 1,
    "descripcion": "Ubicado en la orilla del volcán",
    "superficie": 120,
    "valorDiario": 55000,
    "tipo": "Normal",
    "estado": "Uso",
    "direccion": {
        "comuna": "Antofagasta",
        "calle": "Calle Europa",
        "numero": "125",
        "depto": "22"
    },
    "instalaciones": [
        "Lavandería",
        "Calefacción",
        "Quincho"
    ]
}
```

## DELETE /api/v1/departamento/{id}
Elimina del sistema el departamento especificado por ID.  
- **URL:** http://localhost:5002/api/v1/departamento/{id}
- **Method:** DELETE
- **Respuestas:** 
```
# RESPONSE OK
{
    "message": "Departamento eliminado.",
    "removed": true
}
```  

```
# RESPONSE ID NOT FOUND
{
    "message": "No existe el departamento con ID {id}",
    "removed": false
}
```  

```
# RESPONSE ERROR
{
    "message": "Error al eliminar departamento.",
    "removed": false
}
```  