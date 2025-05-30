using Proyeto.NewFolder;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Data.SqlClient;

namespace Proyeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGenerarCarta_Click(object sender, EventArgs e)
        {
            try
            {
                var carta = new CartaSolicitud
                {
                    Destinatario = txtDestinatario.Text,
                    Firmante = txtFirmante.Text,
                    Fecha = dtpFecha.Value,
                    Motivo = txtMotivo.Text,
                    Telefono = txtTelefono.Text,
                    Dpi = txtDPI.Text
                };

                // Depuración: Mostrar los valores de entrada
                Console.WriteLine($"Destinatario: {carta.Destinatario}, Teléfono: {carta.Telefono}, DPI: {carta.Dpi}");

                string prompt = GenerarPrompt(carta);
                var openAI = new OpenAIService();
                string resultado = await openAI.GenerarRespuestaAsync(prompt);
                rtbResultado.Text = resultado;
                carta.CuerpoCarta = resultado;

                string rutaPlantilla = @"C:\Users\jorge\OneDrive\Desktop\Proyeto\Formato.docx";
                string rutaDestino = $@"C:\Users\jorge\OneDrive\Desktop\Carta_{carta.Motivo}.docx";

                GenerarCartaDesdePlantilla(carta, rutaPlantilla, rutaDestino);
                GuardarEnSQL(carta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la carta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void GuardarEnSQL(CartaSolicitud carta)
        {
            string connectionString = "Server=PC1\\SQLEXPRESS;Database=Generador de Cartas;Trusted_Connection=True;";
            string query = @"INSERT INTO dbo.[Tabla de Registros] 
                ([Motivo de Consulta], [Dirigente], [Fecha de Creacion]) 
                VALUES (@motivo, @destinatario, @fechadecreacion)";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@motivo", carta.Motivo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@destinatario", carta.Destinatario ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@fechadecreacion", carta.Fecha);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private string GenerarPrompt(CartaSolicitud carta)
        {
            return $@"Eres un asistente experto en redacción de cartas formales y personales. Genera el cuerpo principal de una carta en español con el siguiente contexto:
- Destinatario: {carta.Destinatario}
- Firmante: {carta.Firmante}
- Fecha: {carta.Fecha}
- Motivo: {carta.Motivo}

El cuerpo debe:
- Incluir solo el contenido principal que desarrolle el motivo proporcionado (por ejemplo, para 'Solicitud de apoyo para mejora de calle', explica la necesidad y el propósito de la solicitud).
- Usar un tono formal o personalizado según el motivo.
- No incluir alabanzas o elogios innecesarios al destinatario o institución.
- No incluir datos del solicitante ni fecha dentro del motivo.
- Mantener un máximo de 200 palabras.

Ejemplo para 'Solicitud de apoyo para mejora de calle':
El motivo de la presente es solicitar material para la renovación de la calle de “El Barrio Cerro Colorado, Jutiapa, Jutiapa”. Requerimos 1 camionada de piedrín, 1 camionada y media de arena y 20 sacos de cemento. Estos materiales permitirán mejorar el aspecto y funcionalidad de las calles de la comunidad, beneficiando a los residentes.";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void GenerarCartaDesdePlantilla(CartaSolicitud carta, string rutaPlantilla, string rutaDestino)
        {
            try
            {
                // Verificar que la plantilla exista
                if (!File.Exists(rutaPlantilla))
                {
                    throw new FileNotFoundException($"La plantilla no se encuentra en: {rutaPlantilla}");
                }

                // Crear la carpeta de destino si no existe
                string directorioDestino = Path.GetDirectoryName(rutaDestino);
                Directory.CreateDirectory(directorioDestino);

                // Copiar la plantilla al destino
                File.Copy(rutaPlantilla, rutaDestino, true);

                // Abrir el documento Word
                using (WordprocessingDocument doc = WordprocessingDocument.Open(rutaDestino, true))
                {
                    var cuerpo = doc.MainDocumentPart.Document.Body;

                    // Depuración: Mostrar el contenido de la plantilla
                    string textoCompleto = string.Join("", cuerpo.Descendants<Text>().Select(t => t.Text));
                    Console.WriteLine("Contenido de la plantilla antes del reemplazo:");
                    Console.WriteLine(textoCompleto);

                    // Encontrar el párrafo que contiene [MOTIVO]
                    Paragraph motivoParagraph = null;
                    foreach (var paragraph in cuerpo.Descendants<Paragraph>())
                    {
                        if (paragraph.Descendants<Text>().Any(t => t.Text.Contains("[MOTIVO]")))
                        {
                            motivoParagraph = paragraph;
                            break;
                        }
                    }

                    if (motivoParagraph != null)
                    {
                        // Agregar saludo inicial
                        Paragraph saludoParrafo = new Paragraph(new Run(new Text($"Señor, {carta.Destinatario},")));
                        cuerpo.InsertBefore(saludoParrafo, motivoParagraph);

                        // Dividir CuerpoCarta en párrafos
                        var parrafos = (carta.CuerpoCarta ?? "").Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                        // Crear nuevos párrafos para cada línea
                        foreach (var textoParrafo in parrafos)
                        {
                            if (!string.IsNullOrWhiteSpace(textoParrafo))
                            {
                                var nuevoParrafo = new Paragraph
                                {
                                    ParagraphProperties = new ParagraphProperties
                                    {
                                        Justification = new Justification { Val = JustificationValues.Both }
                                    }
                                };
                                nuevoParrafo.Append(new Run(new Text(textoParrafo.Trim())));
                                cuerpo.InsertBefore(nuevoParrafo, motivoParagraph);
                            }
                        }

                        // Eliminar el párrafo original con [MOTIVO]
                        motivoParagraph.Remove();
                    }
                    else
                    {
                        throw new InvalidOperationException("No se encontró el marcador [MOTIVO] en la plantilla.");
                    }

                    // Reemplazar marcadores en todos los nodos Text, manejando nodos divididos
                    foreach (var texto in cuerpo.Descendants<Text>())
                    {
                        string textoOriginal = texto.Text;
                        if (textoOriginal.Contains("[DESTINATARIO]"))
                        {
                            texto.Text = textoOriginal.Replace("[DESTINATARIO]", carta.Destinatario ?? "");
                            Console.WriteLine($"Reemplazado [DESTINATARIO] con '{carta.Destinatario}'");
                        }
                        if (textoOriginal.Contains("[TELÉFONO]"))
                        {
                            texto.Text = textoOriginal.Replace("[TELÉFONO]", string.IsNullOrWhiteSpace(carta.Telefono) ? "[TELÉFONO]" : "Teléfono:" + carta.Telefono);
                            Console.WriteLine($"Reemplazado [TELÉFONO] con '{carta.Telefono}'");
                        }
                        if (textoOriginal.Contains("[FIRMANTE]"))
                        {
                            texto.Text = textoOriginal.Replace("[FIRMANTE]", carta.Firmante ?? "");
                            Console.WriteLine($"Reemplazado [FIRMANTE] con '{carta.Firmante}'");
                        }
                        if (textoOriginal.Contains("[FECHA]"))
                        {
                            texto.Text = textoOriginal.Replace("[FECHA]", carta.Fecha.ToString("dd 'de' MMMM 'de' yyyy"));
                            Console.WriteLine($"Reemplazado [FECHA] con '{carta.Fecha.ToString("dd 'de' MMMM 'de' yyyy")}'");
                        }
                        if (textoOriginal.Contains("[DPI]"))
                        {
                            texto.Text = textoOriginal.Replace("[DPI]", string.IsNullOrWhiteSpace(carta.Dpi) ? "" : "DPI: " + carta.Dpi);
                            Console.WriteLine($"Reemplazado [DPI] con '{carta.Dpi}'");
                        }
                    }

                    // Guardar los cambios
                    doc.MainDocumentPart.Document.Save();
                }

                MessageBox.Show($"Carta generada con formato y guardada en: {rutaDestino}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el documento Word: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarCartaConOpenXml(string contenido, string nombreArchivo)
        {
            try
            {
                // Obtener ruta del escritorio de forma dinámica
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderName = "Cartas Generadas";
                string fullPath = Path.Combine(desktopPath, folderName);

                // Crear carpeta si no existe
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    Console.WriteLine("Carpeta creada en: " + fullPath);
                }

                string archivoFinal = Path.Combine(fullPath, nombreArchivo + ".docx");

                // Crear documento Word
                using (WordprocessingDocument documento = WordprocessingDocument.Create(archivoFinal, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = documento.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body cuerpo = new Body();

                    foreach (string linea in contenido.Split('\n'))
                    {
                        Paragraph parrafo = new Paragraph(new Run(new Text(linea)));
                        cuerpo.Append(parrafo);
                    }

                    mainPart.Document.Append(cuerpo);
                    mainPart.Document.Save();
                }

                MessageBox.Show($"Carta exportada a Word con éxito en: {archivoFinal}", "Exportación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar el documento Word: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}