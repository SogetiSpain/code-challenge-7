# code-challenge-7
Sogeti Code Challenge 7: Marzo 2017
=====================================
Desafío #7: Enviando notas a Firebase
-----------------------------------
Fecha límite: **22 de marzo 2017**

Muchas veces tenemos que interactuar con los servicios externos para no solo leer sino actualizar y editar datos. Firebase es un servicio de Google que permite crear tu propia base de datos para tus programas, independiente de la plataforma. Se puede usar con cualquier lenguaje de programación, gracias a su API basada en REST y JSON.

Nota: Aunque el ejercicio se podría hacer con cualquier servicio parecido, como Azure Mobile Apps (Easy Tables), MongoDB o DocumentDB, os pido que lo hagáis con Firebase por una razón sencilla: aprender a usar otro servicio parecido y no "acomodarnos" en la misma API de siempre.

Podéis pedir una suscripción gratuita de Firebase (nivel Spark) [aquí](https://console.firebase.google.com/)

Requerimientos
--------------
Crea un programa de consola sencillo que permita añadir y mostrar notas, usando Firebase como repositorio de datos. La aplicación se deberá llamar **mynotes** y permitir las siguientes operaciones:

* **mynotes new Comprar agua** debería guardar la nota
* **mynotes show** debería mostrar todas las notas existentes

Ejemplo:
```
  mynotes new Aprender a crear árboles binarios invertidos
  Your note was saved.
  
  mynotes show
  20-02-2017 - Aprender a crear árboles binarios invertidos.
  19-02-2017 - Esto de tomar notas con un programa de consola mola.
```

Restricciones
-------------
*  Crear un fichero de configuración que guarde la clave API de Firebase
*  Usar la [API REST de Firebase](https://firebase.google.com/docs/database/rest/start) en vez de una librería cliente

¿Cómo subir mi código a GitHub?
===============================
En vez de enviar el código a mi correo, tenéis que hacer lo siguiente:
* Hacer un fork de este repositorio (el de SogetiSpain, no el mío personal)
* Crear una carpeta con vuestro nombre
* Crear vuestra solución en esa carpeta
* Hacer _commit_ en vuestro fork
* Hacer un _pull request_ para que lo incluyamos en el repositorio al final del tiempo del desafío

Tenéis una guía de como hacer un fork y pull request en GitHub [aquí](https://help.github.com/articles/fork-a-repo/)




