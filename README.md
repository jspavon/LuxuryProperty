
# Luxury Property

Realizado en .NET core 8, su base de datos es MongoBD la cual esta por medio de un servicio online. Esta no requiere hacer ningún tipo de modificación en el proyecto para reflejar los cambios y datos sin embargo
tiene una limitación de tamaño y transferencia de datos.

# EJECUCION PROYECTO

## VISUAL STUDIO

 1. ABRIR LA SOLUCION EN VISUAL STUDIO

Ejecutar desde el IDE, Este te proporciona una url local la cual podras pegar en el navegador y visualizar Swagger con los endpoint para porder hacer los requests que se necesitan.

##

## VS CODE

1. correr dotnet restore para instalar dependencias
2. correr dotnet run dev para ejecutar el servidor local. Este te proporciona una url local que puedes copiar y pegar en el navegador y visualizar Swagger con los endpoint para porder hacer los requests que se necesitan.

##
# PROCESO PARA LAS PETICIONES

- contamos con un endpoint de usuarios, con la cual tenemos un CRUD. Alli podremos registrar los usuarios que necesitemos para acceder a las peticiones de las api
- las peticiones para Owner, property image, property y property trace cuentan con autorización de JWT token. Para consumir esos endpoint se debe de GENERAR el Access token por medio del endpoin 
  de GENERAR TOKEN. El cual nos va a pedir un userName, este corresponde al cual tengamos en la base de datos registrado. 
- los endpoints de Users no cuentan con restricción por token, por lo cual se pueden consumir sin inconveniente

##

## Authors
Jhon Steven Pavón Bedoya


