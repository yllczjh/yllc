using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 代码生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            string foldPath = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
            }
            #region 生成CS文件
            string result1 = foldPath + @"\" + txt_窗体名.Text + ".cs";//结果保存到桌面
            if (File.Exists(result1))
            {
                File.Create(result1).Dispose();
                File.Delete(result1);
            }
            FileStream fs1 = new FileStream(result1, FileMode.Append);
            StreamWriter wr1 = null;
            wr1 = new StreamWriter(fs1);
            wr1.WriteLine(@"using System.Windows.Forms;
                            namespace " + txt_命名空间.Text + @"
                            {
                                public partial class " + txt_窗体名.Text + @" : Form
                                {");
            wr1.WriteLine(@"public " + txt_窗体名.Text + @"()
                                    {
                                        InitializeComponent();
                                    }");

            wr1.WriteLine(@"}
                            }");
            wr1.Close();
            #endregion


            #region 生成resx文件
            string result2 = foldPath + @"\" + txt_窗体名.Text + ".resx";//结果保存到桌面
            if (File.Exists(result2))
            {
                File.Create(result2).Dispose();
                File.Delete(result2);
            }
            FileStream fs2 = new FileStream(result2, FileMode.Append);
            StreamWriter wr2 = null;
            wr2 = new StreamWriter(fs2);
            wr2.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            wr2.WriteLine("<root>");
            wr2.WriteLine("<!--");
            wr2.WriteLine("Microsoft ResX Schema");
            wr2.WriteLine("Version 2.0");
            wr2.WriteLine(@"The primary goals of this format is to allow a simple XML format
    that is mostly human readable.The generation and parsing of the
    various data types are done through the TypeConverter classes
    associated with the data types.");
            wr2.WriteLine("Example:");
            wr2.WriteLine("... ado.net / XML headers & schema...");
            wr2.WriteLine("<resheader name = \"resmimetype\"> text / microsoft - resx </resheader>");
            wr2.WriteLine("<resheader name = \"version\"> 2.0 </resheader>");
            wr2.WriteLine("<resheader name = \"reader\"> System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>");
            wr2.WriteLine("<resheader name = \"writer\"> System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>");
            wr2.WriteLine("<data name = \"Name1\">< value > this is my long string </value><comment> this is a comment </comment></data>");
            wr2.WriteLine("<data name = \"Color1\" type = \"System.Drawing.Color, System.Drawing\"> Blue </data>");
            wr2.WriteLine("<data name = \"Bitmap1\" mimetype = \"application/x-microsoft.net.object.binary.base64\">");
            wr2.WriteLine("    <value>[base64 mime encoded serialized.NET Framework object] </value>");
            wr2.WriteLine("</data>");
            wr2.WriteLine("<data name = \"Icon1\" type = \"System.Drawing.Icon, System.Drawing\" mimetype = \"application/x-microsoft.net.object.bytearray.base64\">");
            wr2.WriteLine("<value>[base64 mime encoded string representing a byte array form of the.NET Framework object] </value>");
            wr2.WriteLine("<comment> This is a comment </comment>");
            wr2.WriteLine("</data>");
            wr2.WriteLine("There are any number of \"resheader\" rows that contain simple");
            wr2.WriteLine(@"name / value pairs.

    Each data row contains a name, and value.The row also contains a
    type or mimetype.Type corresponds to a.NET class that support
    text/value conversion through the TypeConverter architecture.
    Classes that don't support this are serialized and stored with the 
    mimetype set.


    The mimetype is used for serialized objects, and tells the
    ResXResourceReader how to depersist the object. This is currently not
    extensible.For a given mimetype the value must be set accordingly:


    Note - application/x-microsoft.net.object.binary.base64 is the format
    that the ResXResourceWriter will generate, however the reader can
    read any of the formats listed below.

    mimetype: application/x-microsoft.net.object.binary.base64
    value   : The object must be serialized with
            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            : and then encoded with base64 encoding.


    mimetype: application/x-microsoft.net.object.soap.base64
    value   : The object must be serialized with
            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.bytearray.base64
    value   : The object must be serialized into a byte array 
            : using a System.ComponentModel.TypeConverter
            : and then encoded with base64 encoding.
    -->");
            wr2.WriteLine("<xsd:schema id = \"root\" xmlns= \"\" xmlns:xsd= \"http://www.w3.org/2001/XMLSchema\" xmlns:msdata= \"urn:schemas-microsoft-com:xml-msdata\">");
            wr2.WriteLine("<xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\"/>");
            wr2.WriteLine("<xsd:element name = \"root\" msdata:IsDataSet=\"true\">");
            wr2.WriteLine("<xsd:complexType>");
            wr2.WriteLine("<xsd:choice maxOccurs = \"unbounded\">");
            wr2.WriteLine("<xsd:element name = \"metadata\">");
            wr2.WriteLine("<xsd:complexType>");
            wr2.WriteLine("<xsd:sequence>");
            wr2.WriteLine("<xsd:element name = \"value\" type=\"xsd:string\" minOccurs=\"0\"/> ");
            wr2.WriteLine("</xsd:sequence> ");
            wr2.WriteLine("<xsd:attribute name = \"name\" use=\"required\" type=\"xsd:string\"/>");
            wr2.WriteLine("<xsd:attribute name = \"type\" type=\"xsd:string\"/>");
            wr2.WriteLine("<xsd:attribute name = \"mimetype\" type=\"xsd:string\"/>");
            wr2.WriteLine("<xsd:attribute ref=\"xml:space\"/>");
            wr2.WriteLine("</xsd:complexType>");
            wr2.WriteLine("</xsd:element>");
            wr2.WriteLine("<xsd:element name = \"assembly\">");
            wr2.WriteLine("<xsd:complexType>");
            wr2.WriteLine("<xsd:attribute name = \"alias\" type=\"xsd:string\"/>");
            wr2.WriteLine("<xsd:attribute name = \"name\" type=\"xsd:string\"/>");
            wr2.WriteLine("</xsd:complexType>");
            wr2.WriteLine("</xsd:element>");
            wr2.WriteLine("<xsd:element name = \"data\">");
            wr2.WriteLine("<xsd:complexType>");
            wr2.WriteLine("<xsd:sequence>");
            wr2.WriteLine(" <xsd:element name = \"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />");
            wr2.WriteLine("<xsd:element name = \"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\"/>");
            wr2.WriteLine("</xsd:sequence>");
            wr2.WriteLine("<xsd:attribute name = \"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\"/>");
            wr2.WriteLine("<xsd:attribute name = \"type\" type=\"xsd:string\" msdata:Ordinal=\"3\"/>");
            wr2.WriteLine("<xsd:attribute name = \"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\"/>");
            wr2.WriteLine("<xsd:attribute ref=\"xml:space\"/>");
            wr2.WriteLine(" </xsd:complexType>");
            wr2.WriteLine("</xsd:element>");
            wr2.WriteLine("<xsd:element name = \"resheader\">");
            wr2.WriteLine("<xsd:complexType>");
            wr2.WriteLine("<xsd:sequence>");
            wr2.WriteLine("<xsd:element name = \"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\"/>");
            wr2.WriteLine("</xsd:sequence>");
            wr2.WriteLine("<xsd:attribute name = \"name\" type=\"xsd:string\" use=\"required\"/>");
            wr2.WriteLine(" </xsd:complexType>");
            wr2.WriteLine("</xsd:element>");
            wr2.WriteLine("</xsd:choice>");
            wr2.WriteLine("</xsd:complexType>");
            wr2.WriteLine("</xsd:element>");
            wr2.WriteLine("</xsd:schema>");
            wr2.WriteLine("<resheader name = \"resmimetype\">");
            wr2.WriteLine("<value> text / microsoft - resx </value>");
            wr2.WriteLine("</resheader>");
            wr2.WriteLine("<resheader name=\"version\">");
            wr2.WriteLine("<value>2.0</value>");
            wr2.WriteLine("</resheader>");
            wr2.WriteLine("<resheader name = \"reader\">");
            wr2.WriteLine("<value> System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
            wr2.WriteLine("</resheader>");
            wr2.WriteLine("<resheader name = \"writer\">");
            wr2.WriteLine("<value> System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
            wr2.WriteLine("</resheader>");
            wr2.WriteLine("</root>");
            wr2.Close();
            #endregion

            #region 生成Designer.cs文件
            string result3 = foldPath + @"\" + txt_窗体名.Text + ".Designer.cs";//结果保存到桌面
            if (File.Exists(result3))
            {
                File.Create(result3).Dispose();
                File.Delete(result3);
            }
            FileStream fs3 = new FileStream(result3, FileMode.Append);
            StreamWriter wr3 = null;
            wr3 = new StreamWriter(fs3);
            wr3.WriteLine(@"namespace " + txt_命名空间.Text + @"
                            {
                                partial class " + txt_窗体名.Text + @"
                                {");

            wr3.WriteLine(@"/// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }");

            wr3.WriteLine(@" private void InitializeComponent()
        {
                this.SuspendLayout();
                // 
                // Form2
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(474, 261);");
            wr3.WriteLine(@"this.Name = ""+ txt_窗体名.Text + "";");
            wr3.WriteLine(@"this.Text = "" + txt_窗体名.Text + "";");
            wr3.WriteLine(@"this.ResumeLayout(false);

            }");


            wr3.WriteLine(@"}
                            }");
            wr3.Close();
            #endregion

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_命名空间.Text = "代码生成器";
            txt_窗体名.Text = "Form3";
        }
    }
}
