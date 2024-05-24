using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class BD_gestion : Form
    {
        /* Se crea una nueva instancia de la clase para ocupar de manera local en este formulario */
        Funciones funciones;

        public BD_gestion()
        {
            InitializeComponent();
        }

        // Se crea un nuevo constructor que recibe un parámetro del tipo Funciones
        public BD_gestion(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2;      // Se asigna el valor recibido a la instancia local de Funciones
            ObtenerEmpresas();
            ObtenerEmpresasC();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    //Asignar variables
            funciones.BDGestion = BDGestion.Text;
            funciones.BDContabilidad = BDContabilida.Text;
            funciones.PassConfig= PassConfig.Text; 
            funciones.PassConfigRep = PassConfigRep.Text;

            //Validar que el campo no esta vacio
            if (funciones.CampVacio(BDGestion.Text, "Base de Datos de Gestion") == false &&
                funciones.CampVacio(BDContabilida.Text, "Base de Datos de Contabilidad") == false &&
                funciones.CampVacio(PassConfig.Text, "Contraseña") == false &&
                funciones.CampVacio(PassConfigRep.Text, "Respetir contraseña") == false)
            {
                if (PassConfigRep.Text != PassConfig.Text)
                {
                    MessageBox.Show("La contraseña no coincide, vuelva a intentarlo",
                    "Campo vacio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    CrearTbYLlenado();
                    TriggerActualizar();
                    this.Hide();
                    ConfCorreo vconfCorreo = new ConfCorreo(funciones);
                    vconfCorreo.ShowDialog();
                    this.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            funciones.EscribirLog("info", "Cancelacion de proceso", false, 0);
            //Application.Exit();
            System.Environment.Exit(0);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            VConexionBD vcone = new VConexionBD(funciones);
            vcone.ShowDialog();
            this.Show();
        }

        public void ObtenerEmpresas()
        {
            try
            {
                SqlCommand ObtenerEmpresasGestion = new SqlCommand("SELECT CODEMPRESA, TITULO, PATHBD FROM EMPRESAS", funciones.CnnxICGMx);
                SqlDataReader ReaderObtenerEmpresasGestion = ObtenerEmpresasGestion.ExecuteReader();
                while (ReaderObtenerEmpresasGestion.Read())
                {
                    //BDGestion.Items.Add(ReaderObtenerEmpresasGestion["CODEMPRESA"].ToString() + "  "+ReaderObtenerEmpresasGestion["TITULO"].ToString());
                    BDGestion.Items.Add(ReaderObtenerEmpresasGestion["TITULO"].ToString());
                }
                ReaderObtenerEmpresasGestion.Close();
                funciones.EscribirLog("info", "Obtencion de empresas de gestion de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener las empresas de Gestion. ({e.Message})", true, 1);
            }
        }

        public void ObtenerEmpresasC()
        {
            try
            {
                //SqlCommand ObtenEmConta = new SqlCommand("SELECT CODIGO, DESCRIPCION, PATHBD FROM EMPRESASCONTABLES", funciones.CnnxICGMx);
                SqlCommand ObtenEmConta = new SqlCommand("SELECT RIGHT(PATHBD  , LEN (PATHBD )-CHARINDEX(':', PATHBD) ) AS BASE, *FROM EMPRESASCONTABLES WHERE CODIGO = 11", funciones.CnnxICGMx);

                SqlDataReader ReaderObtenEmConta = ObtenEmConta.ExecuteReader();
                while (ReaderObtenEmConta.Read())
                {
                    BDContabilida.Items.Add(ReaderObtenEmConta["BASE"].ToString());
                }
                ReaderObtenEmConta.Close();
                funciones.EscribirLog("info", "Obtencion de empresas de contabilidad de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener las empresas de Contabilidad. ({e.Message})", true, 1);
            }
        }

        //**************** Actualizar Contabilidad ***********

        public void CrearTbYLlenado()
        {
            try
            {
                //SqlCommand ObtenEmConta = new SqlCommand($"IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME = 'usuarios') " +
                //    $"begin " +
                //        $"SELECT CAST(CODUSUARIO AS nvarchar) +' - '+ USUARIO AS AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL  " +
                //        $"INTO {funciones.BDContabilidad}..Autorizadores " +
                //        $"FROM {funciones.BDGeneral}..USUARIOS " +
                //        $"WHERE CONEXION = @palClave " +
                //    $"end", funciones.CnnxICGMx);

                SqlCommand ObtenEmConta = new SqlCommand($"IF NOT EXISTS (SELECT * FROM {funciones.BDContabilidad}.SYS.OBJECTS WHERE NAME = 'Autorizadores') \r\nBEGIN \r\n\tSELECT CAST(CODUSUARIO AS nvarchar) +' - '+ USUARIO AS AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL  \r\n\tINTO {funciones.BDContabilidad}..Autorizadores FROM GENERAL..USUARIOS WHERE CONEXION = @palClave\r\nEND\r\nELSE\r\nBEGIN\r\n\tDELETE FROM {funciones.BDContabilidad}..Autorizadores\r\n\tINSERT INTO {funciones.BDContabilidad}..Autorizadores (AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL)\r\n\tSELECT CAST(CODUSUARIO AS nvarchar) +' - '+ USUARIO AS AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL  \r\n\tFROM {funciones.BDGeneral}..USUARIOS WHERE CONEXION = @palClave\r\nEND\r\n",funciones.CnnxICGMx);
                ObtenEmConta.Parameters.AddWithValue("@palClave", funciones.palClaveGrupEmp);
                SqlDataReader ReaderObtenEmConta = ObtenEmConta.ExecuteReader();
                while (ReaderObtenEmConta.Read())
                {
                    
                }
                ReaderObtenEmConta.Close();
                funciones.EscribirLog("info", "Creación y llenado de la tabla de Autorizadores de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible la Creación y llenado de la tabla de Autorizadores. ({e.Message})", true, 1);
            }
        }

        public void TriggerActualizar()
        {
            try
            {
                //SqlCommand ObtenEmConta = new SqlCommand($"IF NOT EXISTS (SELECT * FROM sys.triggers WHERE [name] = 'GRUPO_AUTORIZADORES')\r\nBEGIN\r\n\tEXEC SP_EXECUTESQL @SQL = N'\r\nCREATE TRIGGER [dbo].[GRUPO_AUTORIZADORES] ON {{funciones.BDGeneral}}.[dbo].[USUARIOS]\r\nAFTER UPDATE, INSERT AS \r\nBEGIN \r\n\tSET NOCOUNT ON; \r\n\tSET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n\t\r\n\tDECLARE @CODUSUARIO INT, @DESCRIPCION NVARCHAR(50), @EMAIL NVARCHAR(50),@CONEXION_NEW NVARCHAR(50), @CONEXION_OLD NVARCHAR(50) \r\n\t\r\n\tSELECT @CODUSUARIO=CODUSUARIO,@DESCRIPCION=ISNULL(USUARIO,''''),@EMAIL=ISNULL(EMAIL,''''), @CONEXION_NEW=ISNULL(CONEXION,'''') FROM inserted \r\n\tSELECT @CONEXION_OLD=ISNULL(CONEXION,'''') FROM deleted \r\n\t\r\n\tIF @CONEXION_NEW = @CONEXION_OLD AND @CONEXION_NEW = ''@palClave''\r\n\tBEGIN \r\n\t\tUPDATE {{funciones.BDContabilidad}}..Autorizadores SET AUTORIZADOR = (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION), USUARIO = @DESCRIPCION,EMAIL=@EMAIL WHERE CODUSUARIO=@CODUSUARIO \r\n\tEND\r\n\tELSE\r\n\tBEGIN \r\n\t\t\tIF @CONEXION_NEW = '''' OR @CONEXION_NEW <> ''@palClave''\r\n\t\t\tBEGIN\r\n\t\t\t\tDELETE FROM {{funciones.BDContabilidad}}..Autorizadores WHERE CODUSUARIO = @CODUSUARIO\r\n\t\t\tEND\r\n\r\n\t\t\tIF @CONEXION_NEW = ''@palClave''\r\n\t\t\tBEGIN \r\n\t\t\t\tINSERT INTO {{funciones.BDContabilidad}}..Autorizadores (AUTORIZADOR,CODUSUARIO, USUARIO, EMAIL) VALUES (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION,@CODUSUARIO,@DESCRIPCION,@EMAIL)\r\n\t\t\tEND\r\n\tEND\r\n\tSET NOCOUNT OFF;\r\nEND';\r\nEND",funciones.CnnxICGMx);

                //SqlCommand ObtenEmConta = new SqlCommand($"IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[GRUPO_AUTORIZADORES]'))\r\nBEGIN\r\nEXEC SP_EXECUTESQL @SQL = N'\r\nALTER TRIGGER [dbo].[GRUPO_AUTORIZADORES] ON [dbo].[USUARIOS]\r\n AFTER UPDATE, INSERT AS   \r\n BEGIN    \r\n  SET NOCOUNT ON;    \r\n  SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED    \r\n  \r\n  DECLARE @CODUSUARIO INT, @DESCRIPCION NVARCHAR(50), @EMAIL NVARCHAR(50), @CONEXION_NEW NVARCHAR(50), @CONEXION_OLD NVARCHAR(50)\r\n  \r\n  SELECT @CODUSUARIO=CODUSUARIO,@DESCRIPCION=ISNULL(USUARIO,'''') ,@EMAIL=ISNULL(EMAIL,''''), @CONEXION_NEW=ISNULL(CONEXION,'''') FROM inserted    \r\n  SELECT @CONEXION_OLD=ISNULL(CONEXION,'''') FROM deleted     \r\n  \r\n  IF @CONEXION_NEW = @CONEXION_OLD AND @CONEXION_NEW = ''@palClave''\r\n  BEGIN\r\n\tUPDATE {funciones.BDContabilidad}..Autorizadores SET AUTORIZADOR = (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION), USUARIO = @DESCRIPCION,EMAIL=@EMAIL  WHERE CODUSUARIO=@CODUSUARIO \r\n  END\r\n  ELSE\r\n  BEGIN\r\n\t  IF @CONEXION_NEW = '''' OR @CONEXION_NEW <> ''@palClave''\r\n\t  BEGIN\r\n\t\tDELETE FROM {funciones.BDContabilidad}..Autorizadores WHERE CODUSUARIO = @CODUSUARIO\r\n\t  END\r\n\t \r\n\t  IF @CONEXION_NEW = ''@palClave''\r\n\t  BEGIN\r\n\t\tINSERT INTO {funciones.BDContabilidad}..Autorizadores (AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL) VALUES (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION,@CODUSUARIO,@DESCRIPCION,@EMAIL)\r\n\t  END\r\n  END \r\n\r\n  SET NOCOUNT OFF;\r\n\r\nEND';\r\nEND\r\nELSE \r\nBEGIN\r\nEXEC SP_EXECUTESQL @SQL = N'\r\nCREATE TRIGGER [dbo].[GRUPO_AUTORIZADORES] ON [dbo].[USUARIOS]\r\n AFTER UPDATE, INSERT AS   \r\n BEGIN    \r\n  SET NOCOUNT ON;    \r\n  SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED    \r\n  \r\n  DECLARE @CODUSUARIO INT, @DESCRIPCION NVARCHAR(50), @EMAIL NVARCHAR(50), @CONEXION_NEW NVARCHAR(50), @CONEXION_OLD NVARCHAR(50)\r\n  \r\n  SELECT @CODUSUARIO=CODUSUARIO,@DESCRIPCION=ISNULL(USUARIO,'''') ,@EMAIL=ISNULL(EMAIL,''''), @CONEXION_NEW=ISNULL(CONEXION,'''') FROM inserted    \r\n  SELECT @CONEXION_OLD=ISNULL(CONEXION,'''') FROM deleted     \r\n  \r\n  IF @CONEXION_NEW = @CONEXION_OLD AND @CONEXION_NEW = ''@palClave''\r\n  BEGIN\r\n\tUPDATE {funciones.BDContabilidad}..Autorizadores SET AUTORIZADOR = (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION), USUARIO = @DESCRIPCION,EMAIL=@EMAIL  WHERE CODUSUARIO=@CODUSUARIO \r\n  END\r\n  ELSE\r\n  BEGIN\r\n\t  IF @CONEXION_NEW = '''' OR @CONEXION_NEW <> ''@palClave''\r\n\t  BEGIN\r\n\t\tDELETE FROM {funciones.BDContabilidad}..Autorizadores WHERE CODUSUARIO = @CODUSUARIO\r\n\t  END\r\n\t \r\n\t  IF @CONEXION_NEW = ''@palClave''\r\n\t  BEGIN\r\n\t\tINSERT INTO {funciones.BDContabilidad}..Autorizadores (AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL) VALUES (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION,@CODUSUARIO,@DESCRIPCION,@EMAIL)\r\n\t  END\r\n  END \r\n\r\n  SET NOCOUNT OFF;\r\n\r\nEND';\r\nEND", funciones.CnnxICGMx);
                SqlCommand ObtenEmConta = new SqlCommand($"IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[GRUPO_AUTORIZADORES]'))\r\nBEGIN\r\nEXEC SP_EXECUTESQL @SQL = N'\r\nALTER TRIGGER [dbo].[GRUPO_AUTORIZADORES] ON [dbo].[USUARIOS]\r\n AFTER UPDATE, INSERT AS   \r\n BEGIN    \r\n  SET NOCOUNT ON;    \r\n  SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED    \r\n  \r\n  DECLARE @CODUSUARIO INT, @DESCRIPCION NVARCHAR(50), @EMAIL NVARCHAR(50), @CONEXION_NEW NVARCHAR(50), @CONEXION_OLD NVARCHAR(50)\r\n  \r\n  SELECT @CODUSUARIO=CODUSUARIO,@DESCRIPCION=ISNULL(USUARIO,'''') ,@EMAIL=ISNULL(EMAIL,''''), @CONEXION_NEW=ISNULL(CONEXION,'''') FROM inserted    \r\n  SELECT @CONEXION_OLD=ISNULL(CONEXION,'''') FROM deleted     \r\n  \r\n  IF @CONEXION_NEW = @CONEXION_OLD AND @CONEXION_NEW = ''{funciones.palClaveGrupEmp}''\r\n  BEGIN\r\n\tUPDATE {funciones.BDContabilidad}..Autorizadores SET AUTORIZADOR = (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION), USUARIO = @DESCRIPCION,EMAIL=@EMAIL  WHERE CODUSUARIO=@CODUSUARIO \r\n  END\r\n  ELSE\r\n  BEGIN\r\n\t  IF @CONEXION_NEW = '''' OR @CONEXION_NEW <> ''{funciones.palClaveGrupEmp}''\r\n\t  BEGIN\r\n\t\tDELETE FROM {funciones.BDContabilidad}..Autorizadores WHERE CODUSUARIO = @CODUSUARIO\r\n\t  END\r\n\t \r\n\t  IF @CONEXION_NEW = ''{funciones.palClaveGrupEmp}''\r\n\t  BEGIN\r\n\t\tINSERT INTO {funciones.BDContabilidad}..Autorizadores (AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL) VALUES (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION,@CODUSUARIO,@DESCRIPCION,@EMAIL)\r\n\t  END\r\n  END \r\n\r\n  SET NOCOUNT OFF;\r\n\r\nEND';\r\nEND\r\nELSE \r\nBEGIN\r\nEXEC SP_EXECUTESQL @SQL = N'\r\nCREATE TRIGGER [dbo].[GRUPO_AUTORIZADORES] ON [dbo].[USUARIOS]\r\n AFTER UPDATE, INSERT AS   \r\n BEGIN    \r\n  SET NOCOUNT ON;    \r\n  SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED    \r\n  \r\n  DECLARE @CODUSUARIO INT, @DESCRIPCION NVARCHAR(50), @EMAIL NVARCHAR(50), @CONEXION_NEW NVARCHAR(50), @CONEXION_OLD NVARCHAR(50)\r\n  \r\n  SELECT @CODUSUARIO=CODUSUARIO,@DESCRIPCION=ISNULL(USUARIO,'''') ,@EMAIL=ISNULL(EMAIL,''''), @CONEXION_NEW=ISNULL(CONEXION,'''') FROM inserted    \r\n  SELECT @CONEXION_OLD=ISNULL(CONEXION,'''') FROM deleted     \r\n  \r\n  IF @CONEXION_NEW = @CONEXION_OLD AND @CONEXION_NEW = ''{funciones.palClaveGrupEmp}''\r\n  BEGIN\r\n\tUPDATE {funciones.BDContabilidad}..Autorizadores SET AUTORIZADOR = (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION), USUARIO = @DESCRIPCION,EMAIL=@EMAIL  WHERE CODUSUARIO=@CODUSUARIO \r\n  END\r\n  ELSE\r\n  BEGIN\r\n\t  IF @CONEXION_NEW = '''' OR @CONEXION_NEW <> ''{funciones.palClaveGrupEmp}''\r\n\t  BEGIN\r\n\t\tDELETE FROM {funciones.BDContabilidad}..Autorizadores WHERE CODUSUARIO = @CODUSUARIO\r\n\t  END\r\n\t \r\n\t  IF @CONEXION_NEW = ''{funciones.palClaveGrupEmp}''\r\n\t  BEGIN\r\n\t\tINSERT INTO {funciones.BDContabilidad}..Autorizadores (AUTORIZADOR, CODUSUARIO, USUARIO, EMAIL) VALUES (CAST(@CODUSUARIO AS NVARCHAR) + '' - ''+ @DESCRIPCION,@CODUSUARIO,@DESCRIPCION,@EMAIL)\r\n\t  END\r\n  END \r\n\r\n  SET NOCOUNT OFF;\r\n\r\nEND';\r\nEND", funciones.CnnxICGMx);
                ObtenEmConta.Parameters.AddWithValue("@palClave", funciones.palClaveGrupEmp);
                SqlDataReader ReaderObtenEmConta = ObtenEmConta.ExecuteReader();
                while (ReaderObtenEmConta.Read())
                {
                    
                }
                ReaderObtenEmConta.Close();
                funciones.EscribirLog("info", "Ejecución del Trigger de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible ejecutar el Trigreer. ({e.Message})", true, 1);
            }
        }
        
        //****************************************************

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void BDGestion_SelectedIndexChanged(object sender, EventArgs e) { }
        private void BD_gestion_Load(object sender, EventArgs e) 
        {
            BDGestion.Text = funciones.BDGestion;
            BDContabilida.Text = funciones.BDContabilidad;
            PassConfig.Text = funciones.PassConfig;
            PassConfigRep.Text = funciones.PassConfigRep;

        }
        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void BD_gestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(BDGestion", false, 0);
                System.Environment.Exit(0);
            //}
        }
    }
}




