using HealthCardManager;
using HealthCardUtil.Tool;
using HisCommon.DataEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HealthCardTestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string res_xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            string   res_xml1001 = res_xml+ @"<ROOT>
                <FUN_CODE><![CDATA[1001]]></FUN_CODE>
                <USER_ID><![CDATA[41275532X]]></USER_ID>
                <SIGN_TYPE><![CDATA[MD5]]></SIGN_TYPE>
                <SIGN><![CDATA[CC220A9C1E1BB8BAF3F57D7BB04257E2]]></SIGN>
                <REQ_ENCRYPTED><![CDATA[167sUrVqNgVZ+4ehf9nDGdH7s2Zjp3o9l59Ybl2QwRoyN0wRyvlEMNc4dZeMgEoe/nz8mOGDlSGQEfNGrHoZcg==]]></REQ_ENCRYPTED>
              </ROOT>";

            string  res_xml1004 = res_xml + @"<ROOT>
                <FUN_CODE><![CDATA[1004]]></FUN_CODE>
                <USER_ID><![CDATA[41275532X]]></USER_ID>
                <SIGN_TYPE><![CDATA[MD5]]></SIGN_TYPE>
                <SIGN><![CDATA[2EA0498D2DC8F79C27056462C7549507]]></SIGN>
                <REQ_ENCRYPTED><![CDATA[l0JwVg+tj8nW9xWerp1WLCcblL0hrMoHX+D5jHHx9Z9yO9niWIS+ERZR/ZuLSGKk]]></REQ_ENCRYPTED>
              </ROOT>";
            string ss = @"<?xml version='1.0' encoding='utf-8' ?><ROOT><FUN_CODE>8001</FUN_CODE><USER_ID>ln_12320wx</USER_ID><SIGN>4E6D3AE1B4CC091D3E3A4F07A35DA2DD</SIGN><SIGN_TYPE>MD5</SIGN_TYPE><REQ_ENCRYPTED>86DNzDw5b327rIcPqjdQjRdrzUw4sOtbdGwXFWh8hMJ/l+wxRAkfKv641Hh+aoZ7ky/Nda4Uoat6A9ps/z82tHlsIjTchl/FBWeTcHNpmt/pJi3aE501KgeZ2h2UTcLW1t7kVzp6PQ2Q91YpCmOZffT6DMvu47fqiLQehF1FqPWXM3V+oVontLIGXK08oIau8zpgnPIdi23B8y4hYilD+OiwOKrZZwmHy4CKCA4NJ8fasKFusj4RiXYsK7AIHf1if1mWh/jDwGsTfdTBaijePDeSex5uEc+q4MEHVA/iBwm76pBVh+2maiYHmm0q9V3uTUCyEtAGO8bX2Ep/9evIEiBK1yLghXPaUA7hZja66AbigJ85Qxkdi7DRvQBKM3jqtH/Zv64lKtRtTuEKN2EUeaoQDH67pK40gx3ZkwucDylH/e/ZiJBTaslBjzsFqA18E6csqAPBW09E9KOphjN0vA==</REQ_ENCRYPTED></ROOT>";
            //HEALTHCARD_PATIENT_INFO patInfo = new HEALTHCARD_PATIENT_INFO();
            //patInfo.HOS_ID = "41275532X";
            //patInfo.Mobile = "18622093158";
            //patInfo.HEALTH_CARD_ID = "EF2FD39A4F574D0F8D4739211C3EF4D684408F9BFE1505DA8FB7F382FC8BC37D";

            //var  tt=   HisHelper.CreatePatInfo(patInfo);
            string resvs =  HealthCardManager.HttpHelper.HealthCardService(ss);

            var outParam = HisHelper.GetRegInfo("0101", "-1", Convert.ToDateTime("2019-06-01"), Convert.ToDateTime("2019-12-01"));
            //  var outParam = HisHelper.GetTimeRegInfo("0109", "-1", Convert.ToDateTime("2019-11-29"), "-1");
            //    string s = HttpHelper.HealthCardService(ss);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Text = HealthCardUtil.Security.AESHelper.EncryptForAES(this.richTextBox1.Text, "2098D32C4D1399EC");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = HealthCardUtil.Security.AESHelper.DecryptForAES(this.richTextBox2.Text, "2098D32C4D1399EC");
        }
    }
}
