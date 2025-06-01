                                            DESCRIPCION DEL FUNCIONAMIENTO

Este programa es una aplicación de escritorio en C# que ayuda a generar cartas personalizadas de manera automática. El usuario ingresa datos como destinatario, motivo, firmante, teléfono, DPI y fecha. Luego, la app utiliza la API de OpenAI con el modelo "o4-mini" para redactar el cuerpo de la carta de manera formal y coherente.

El texto generado se inserta en una plantilla de Word, reemplazando marcadores como [DESTINATARIO], [MOTIVO], [FIRMANTE], entre otros. Finalmente, la carta se guarda como archivo Word en el escritor del dispositivo en donde este el programa y se almacenan en una base de datos SQL Server los datos principales: motivo, destinatario y fecha de creación. Así, todo queda registrado y listo para su consulta.

[Click Aqui para ver el video del funcionamiento](https://drive.google.com/file/d/1SGydxIvqPQzVlhr5FzXx96H70UxX1E1U/view?usp=drivesdk)
