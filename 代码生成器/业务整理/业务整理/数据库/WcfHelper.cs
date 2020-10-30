using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
namespace 业务管理.数据库
{
    public class WcfHelper
    {

        /// <summary>
        /// 参数对象
        /// </summary>
        public class ParmObj
        {
            public string p0 = "";
            public string p1 = "";
            public string p2 = "";
            public string p3 = "";
            public string p4 = "";
            public string p5 = "";
            public string p6 = "";
            public string p7 = "";
            public string p8 = "";
            public string p9 = "";
            public string p10 = "";
            public string p11 = "";
            public string p12 = "";
            public string p13 = "";
            public string p14 = "";
            public string p15 = "";
            public string p16 = "";
            public string p17 = "";
            public string p18 = "";
            public string p19 = "";
            public string p20 = "";
            public string p21 = "";
            public string p22 = "";
            public string p23 = "";
            public string p24 = "";
            public string p25 = "";
            public string p26 = "";
            public string p27 = "";
            public string p28 = "";
            public string p29 = "";
            public string p30 = "";
            public string p31 = "";
            public string p32 = "";
            public string p33 = "";
            public string p34 = "";
            public string p35 = "";
            public string p36 = "";
            public string p37 = "";
            public string p38 = "";
            public string p39 = "";
            public string p40 = "";
            public string p41 = "";
            public string p42 = "";
            public string p43 = "";
            public string p44 = "";
            public string p45 = "";
            public string p46 = "";
            public string p47 = "";
            public string p48 = "";
            public string p49 = "";
            public string p50 = "";
            public string p51 = "";
            public string p52 = "";
            public string p53 = "";
            public string p54 = "";
            public string p55 = "";
            public string p56 = "";
            public string p57 = "";
            public string p58 = "";
            public string p59 = "";
            public string p60 = "";

            public string p61 = "";
            public string p62 = "";
            public string p63 = "";
            public string p64 = "";
            public string p65 = "";
            public string p66 = "";
            public string p67 = "";
            public string p68 = "";
            public string p69 = "";
            public string p70 = "";
            public string p71 = "";
            public string p72 = "";
            public string p73 = "";
            public string p74 = "";
            public string p75 = "";
            public string p76 = "";
            public string p77 = "";
            public string p78 = "";
            public string p79 = "";
            public string p80 = "";

            #region 2013年3月1日刘冬阳添加，明确参数功能

            public string WCF入参1 = "";
            public string WCF入参2 = "";
            public string WCF入参3 = "";

            public string WCF出参 = "";

            /// <summary>
            /// 执行成功添"true"，失败添"false",注意大小写！
            /// </summary>
            private string _isSuccess = "false";

            public bool
                IsSuccess
            {
                get { return _isSuccess.ToLower() == "true"; }

                set
                {
                    if (value) _isSuccess = "true";
                    else
                    {
                        _isSuccess = "false";
                    }
                }
            }

            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMessage = "";

            public ParmObj Copy()
            {
                ParmObj p = new ParmObj();

                p.p0 = string.Copy(this.p0);
                p.p1 = string.Copy(this.p1);
                p.p2 = string.Copy(this.p2);
                p.p3 = string.Copy(this.p3);
                p.p4 = string.Copy(this.p4);
                p.p5 = string.Copy(this.p5);
                p.p6 = string.Copy(this.p6);
                p.p7 = string.Copy(this.p7);
                p.p8 = string.Copy(this.p8);
                p.p9 = string.Copy(this.p9);
                p.p10 = string.Copy(this.p10);
                p.p11 = string.Copy(this.p11);
                p.p12 = string.Copy(this.p12);
                p.p13 = string.Copy(this.p13);
                p.p14 = string.Copy(this.p14);
                p.p15 = string.Copy(this.p15);
                p.p16 = string.Copy(this.p16);
                p.p17 = string.Copy(this.p17);
                p.p18 = string.Copy(this.p18);
                p.p19 = string.Copy(this.p19);
                p.p20 = string.Copy(this.p20);
                p.p21 = string.Copy(this.p21);
                p.p22 = string.Copy(this.p22);
                p.p23 = string.Copy(this.p23);
                p.p24 = string.Copy(this.p24);
                p.p25 = string.Copy(this.p25);
                p.p26 = string.Copy(this.p26);
                p.p27 = string.Copy(this.p27);
                p.p28 = string.Copy(this.p28);
                p.p29 = string.Copy(this.p29);
                p.p30 = string.Copy(this.p30);
                p.p31 = string.Copy(this.p31);
                p.p32 = string.Copy(this.p32);
                p.p33 = string.Copy(this.p33);
                p.p34 = string.Copy(this.p34);
                p.p35 = string.Copy(this.p35);
                p.p36 = string.Copy(this.p36);
                p.p37 = string.Copy(this.p37);
                p.p38 = string.Copy(this.p38);
                p.p39 = string.Copy(this.p39);
                p.p40 = string.Copy(this.p40);
                p.p41 = string.Copy(this.p41);
                p.p42 = string.Copy(this.p42);
                p.p43 = string.Copy(this.p43);
                p.p44 = string.Copy(this.p44);
                p.p45 = string.Copy(this.p45);
                p.p46 = string.Copy(this.p46);
                p.p47 = string.Copy(this.p47);
                p.p48 = string.Copy(this.p48);
                p.p49 = string.Copy(this.p49);
                p.p50 = string.Copy(this.p50);
                p.p51 = string.Copy(this.p51);
                p.p52 = string.Copy(this.p52);
                p.p53 = string.Copy(this.p53);
                p.p54 = string.Copy(this.p54);
                p.p55 = string.Copy(this.p55);
                p.p56 = string.Copy(this.p56);
                p.p57 = string.Copy(this.p57);
                p.p58 = string.Copy(this.p58);
                p.p59 = string.Copy(this.p59);
                p.p60 = string.Copy(this.p60);
                p.p61 = string.Copy(this.p61);
                p.p62 = string.Copy(this.p62);
                p.p63 = string.Copy(this.p63);
                p.p64 = string.Copy(this.p64);
                p.p65 = string.Copy(this.p65);
                p.p66 = string.Copy(this.p66);
                p.p67 = string.Copy(this.p67);
                p.p68 = string.Copy(this.p68);
                p.p69 = string.Copy(this.p69);
                p.p70 = string.Copy(this.p70);
                p.p71 = string.Copy(this.p71);
                p.p72 = string.Copy(this.p72);
                p.p73 = string.Copy(this.p73);
                p.p74 = string.Copy(this.p74);
                p.p75 = string.Copy(this.p75);
                p.p76 = string.Copy(this.p76);
                p.p77 = string.Copy(this.p77);
                p.p78 = string.Copy(this.p78);
                p.p79 = string.Copy(this.p79);
                p.p80 = string.Copy(this.p80);
                p.WCF入参1 = string.Copy(this.WCF入参1);
                p.WCF入参2 = string.Copy(this.WCF入参2);
                p.WCF入参3 = string.Copy(this.WCF入参3);
                p.IsSuccess = this.IsSuccess;
                p.ErrorMessage = string.Copy(this.ErrorMessage);
                p.WCF出参 = string.Copy(this.WCF出参);

                return p;
            }

            #endregion


            /// <summary>
            /// 把20个参数清空
            /// </summary>
            public void Clear()
            {
                p0 = "";
                p1 = "";
                p2 = "";
                p3 = "";
                p4 = "";
                p5 = "";
                p6 = "";
                p7 = "";
                p8 = "";
                p9 = "";
                p10 = "";
                p11 = "";
                p12 = "";
                p13 = "";
                p14 = "";
                p15 = "";
                p16 = "";
                p17 = "";
                p18 = "";
                p19 = "";
                p20 = "";
                p21 = "";
                p22 = "";
                p23 = "";
                p24 = "";
                p25 = "";
                p26 = "";
                p27 = "";
                p28 = "";
                p29 = "";
                p30 = "";
                p31 = "";
                p32 = "";
                p33 = "";
                p34 = "";
                p35 = "";
                p36 = "";
                p37 = "";
                p38 = "";
                p39 = "";
                p40 = "";
                p41 = "";
                p42 = "";
                p43 = "";
                p44 = "";
                p45 = "";
                p46 = "";
                p47 = "";
                p48 = "";
                p49 = "";
                p50 = "";
                p51 = "";
                p52 = "";
                p53 = "";
                p54 = "";
                p55 = "";
                p56 = "";
                p57 = "";
                p58 = "";
                p59 = "";
                p60 = "";
                p61 = "";
                p62 = "";
                p63 = "";
                p64 = "";
                p65 = "";
                p66 = "";
                p67 = "";
                p68 = "";
                p69 = "";
                p70 = "";
                p71 = "";
                p72 = "";
                p73 = "";
                p74 = "";
                p75 = "";
                p76 = "";
                p77 = "";
                p78 = "";
                p79 = "";
                p80 = "";
                WCF入参1 = "";
                WCF入参2 = "";
                WCF入参3 = "";
                IsSuccess = false;
                ErrorMessage = "false";
                WCF出参 = "";

            }

        }
        /// <summary>
        /// 参数对象转换成字符串
        /// </summary>
        /// <param name="Pobj">参数对象</param>
        /// <returns></returns>
        public static string ParmobjToStr(ParmObj Pobj)
        {
            StringBuilder sb = new StringBuilder();

            if (Pobj == null)
            {
                return "#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!#!";
            }
            for (int i = 0; i <= 86; i++)
            {
                switch (i)
                {

                    case 0:
                        if (Pobj.p0 == null)
                        {
                            Pobj.p0 = "";
                        }
                        sb.Append(Pobj.p0 + "#!");
                        break;
                    case 1:
                        if (Pobj.p1 == null)
                        {
                            Pobj.p1 = "";
                        }
                        sb.Append(Pobj.p1 + "#!");
                        break;
                    case 2:
                        if (Pobj.p2 == null)
                        {
                            Pobj.p2 = "";
                        }
                        sb.Append(Pobj.p2 + "#!");
                        break;
                    case 3:
                        if (Pobj.p3 == null)
                        {
                            Pobj.p3 = "";
                        }
                        sb.Append(Pobj.p3 + "#!");
                        break;
                    case 4:
                        if (Pobj.p4 == null)
                        {
                            Pobj.p4 = "";
                        }
                        sb.Append(Pobj.p4 + "#!");
                        break;
                    case 5:
                        if (Pobj.p5 == null)
                        {
                            Pobj.p5 = "";
                        }
                        sb.Append(Pobj.p5 + "#!");
                        break;
                    case 6:
                        if (Pobj.p6 == null)
                        {
                            Pobj.p6 = "";
                        }
                        sb.Append(Pobj.p6 + "#!");
                        break;
                    case 7:
                        if (Pobj.p7 == null)
                        {
                            Pobj.p7 = "";
                        }
                        sb.Append(Pobj.p7 + "#!");
                        break;
                    case 8:
                        if (Pobj.p8 == null)
                        {
                            Pobj.p8 = "";
                        }
                        sb.Append(Pobj.p8 + "#!");
                        break;
                    case 9:
                        if (Pobj.p9 == null)
                        {
                            Pobj.p9 = "";
                        }
                        sb.Append(Pobj.p9 + "#!");
                        break;
                    case 10:
                        if (Pobj.p10 == null)
                        {
                            Pobj.p10 = "";
                        }
                        sb.Append(Pobj.p10 + "#!");
                        break;
                    case 11:
                        if (Pobj.p11 == null)
                        {
                            Pobj.p11 = "";
                        }
                        sb.Append(Pobj.p11 + "#!");
                        break;
                    case 12:
                        if (Pobj.p12 == null)
                        {
                            Pobj.p12 = "";
                        }
                        sb.Append(Pobj.p12 + "#!");
                        break;
                    case 13:
                        if (Pobj.p13 == null)
                        {
                            Pobj.p13 = "";
                        }
                        sb.Append(Pobj.p13 + "#!");
                        break;
                    case 14:
                        if (Pobj.p14 == null)
                        {
                            Pobj.p14 = "";
                        }
                        sb.Append(Pobj.p14 + "#!");
                        break;
                    case 15:
                        if (Pobj.p15 == null)
                        {
                            Pobj.p15 = "";
                        }
                        sb.Append(Pobj.p15 + "#!");
                        break;
                    case 16:
                        if (Pobj.p16 == null)
                        {
                            Pobj.p16 = "";
                        }
                        sb.Append(Pobj.p16 + "#!");
                        break;
                    case 17:
                        if (Pobj.p17 == null)
                        {
                            Pobj.p17 = "";
                        }
                        sb.Append(Pobj.p17 + "#!");
                        break;
                    case 18:
                        if (Pobj.p18 == null)
                        {
                            Pobj.p18 = "";
                        }
                        sb.Append(Pobj.p18 + "#!");
                        break;
                    case 19:
                        if (Pobj.p19 == null)
                        {
                            Pobj.p19 = "";
                        }
                        sb.Append(Pobj.p19 + "#!");
                        break;
                    case 20:
                        if (Pobj.p20 == null)
                        {
                            Pobj.p20 = "";
                        }
                        sb.Append(Pobj.p20 + "#!");
                        break;
                    case 21:
                        if (Pobj.p21 == null)
                        {
                            Pobj.p21 = "";
                        }
                        sb.Append(Pobj.p21 + "#!");
                        break;
                    case 22:
                        if (Pobj.p22 == null)
                        {
                            Pobj.p22 = "";
                        }
                        sb.Append(Pobj.p22 + "#!");
                        break;
                    case 23:
                        if (Pobj.p23 == null)
                        {
                            Pobj.p23 = "";
                        }
                        sb.Append(Pobj.p23 + "#!");
                        break;
                    case 24:
                        if (Pobj.p24 == null)
                        {
                            Pobj.p24 = "";
                        }
                        sb.Append(Pobj.p24 + "#!");
                        break;
                    case 25:
                        if (Pobj.p25 == null)
                        {
                            Pobj.p25 = "";
                        }
                        sb.Append(Pobj.p25 + "#!");
                        break;
                    case 26:
                        if (Pobj.p26 == null)
                        {
                            Pobj.p26 = "";
                        }
                        sb.Append(Pobj.p26 + "#!");
                        break;
                    case 27:
                        if (Pobj.p27 == null)
                        {
                            Pobj.p27 = "";
                        }
                        sb.Append(Pobj.p27 + "#!");
                        break;
                    case 28:
                        if (Pobj.p28 == null)
                        {
                            Pobj.p28 = "";
                        }
                        sb.Append(Pobj.p28 + "#!");
                        break;
                    case 29:
                        if (Pobj.p29 == null)
                        {
                            Pobj.p29 = "";
                        }
                        sb.Append(Pobj.p29 + "#!");
                        break;
                    case 30:
                        if (Pobj.p30 == null)
                        {
                            Pobj.p30 = "";
                        }
                        sb.Append(Pobj.p30 + "#!");
                        break;
                    case 31:
                        if (Pobj.p31 == null)
                        {
                            Pobj.p31 = "";
                        }
                        sb.Append(Pobj.p31 + "#!");
                        break;
                    case 32:
                        if (Pobj.p32 == null)
                        {
                            Pobj.p32 = "";
                        }
                        sb.Append(Pobj.p32 + "#!");
                        break;
                    case 33:
                        if (Pobj.p33 == null)
                        {
                            Pobj.p33 = "";
                        }
                        sb.Append(Pobj.p33 + "#!");
                        break;
                    case 34:
                        if (Pobj.p34 == null)
                        {
                            Pobj.p34 = "";
                        }
                        sb.Append(Pobj.p34 + "#!");
                        break;
                    case 35:
                        if (Pobj.p35 == null)
                        {
                            Pobj.p35 = "";
                        }
                        sb.Append(Pobj.p35 + "#!");
                        break;
                    case 36:
                        if (Pobj.p36 == null)
                        {
                            Pobj.p36 = "";
                        }
                        sb.Append(Pobj.p36 + "#!");
                        break;
                    case 37:
                        if (Pobj.p37 == null)
                        {
                            Pobj.p37 = "";
                        }
                        sb.Append(Pobj.p37 + "#!");
                        break;
                    case 38:
                        if (Pobj.p38 == null)
                        {
                            Pobj.p38 = "";
                        }
                        sb.Append(Pobj.p38 + "#!");
                        break;
                    case 39:
                        if (Pobj.p39 == null)
                        {
                            Pobj.p39 = "";
                        }
                        sb.Append(Pobj.p39 + "#!");
                        break;
                    case 40:
                        if (Pobj.p40 == null)
                        {
                            Pobj.p40 = "";
                        }
                        sb.Append(Pobj.p40 + "#!");
                        break;
                    case 41:
                        if (Pobj.p41 == null)
                        {
                            Pobj.p41 = "";
                        }
                        sb.Append(Pobj.p41 + "#!");
                        break;
                    case 42:
                        if (Pobj.p42 == null)
                        {
                            Pobj.p42 = "";
                        }
                        sb.Append(Pobj.p42 + "#!");
                        break;
                    case 43:
                        if (Pobj.p43 == null)
                        {
                            Pobj.p43 = "";
                        }
                        sb.Append(Pobj.p43 + "#!");
                        break;
                    case 44:
                        if (Pobj.p44 == null)
                        {
                            Pobj.p44 = "";
                        }
                        sb.Append(Pobj.p44 + "#!");
                        break;
                    case 45:
                        if (Pobj.p45 == null)
                        {
                            Pobj.p45 = "";
                        }
                        sb.Append(Pobj.p45 + "#!");
                        break;
                    case 46:
                        if (Pobj.p46 == null)
                        {
                            Pobj.p46 = "";
                        }
                        sb.Append(Pobj.p46 + "#!");
                        break;
                    case 47:
                        if (Pobj.p47 == null)
                        {
                            Pobj.p47 = "";
                        }
                        sb.Append(Pobj.p47 + "#!");
                        break;
                    case 48:
                        if (Pobj.p48 == null)
                        {
                            Pobj.p48 = "";
                        }
                        sb.Append(Pobj.p48 + "#!");
                        break;
                    case 49:
                        if (Pobj.p49 == null)
                        {
                            Pobj.p49 = "";
                        }
                        sb.Append(Pobj.p49 + "#!");
                        break;
                    case 50:
                        if (Pobj.p50 == null)
                        {
                            Pobj.p50 = "";
                        }
                        sb.Append(Pobj.p50 + "#!");
                        break;
                    case 51:
                        if (Pobj.p51 == null)
                        {
                            Pobj.p51 = "";
                        }
                        sb.Append(Pobj.p51 + "#!");
                        break;
                    case 52:
                        if (Pobj.p52 == null)
                        {
                            Pobj.p52 = "";
                        }
                        sb.Append(Pobj.p52 + "#!");
                        break;
                    case 53:
                        if (Pobj.p53 == null)
                        {
                            Pobj.p53 = "";
                        }
                        sb.Append(Pobj.p53 + "#!");
                        break;
                    case 54:
                        if (Pobj.p54 == null)
                        {
                            Pobj.p54 = "";
                        }
                        sb.Append(Pobj.p54 + "#!");
                        break;
                    case 55:
                        if (Pobj.p55 == null)
                        {
                            Pobj.p55 = "";
                        }
                        sb.Append(Pobj.p55 + "#!");
                        break;
                    case 56:
                        if (Pobj.p56 == null)
                        {
                            Pobj.p56 = "";
                        }
                        sb.Append(Pobj.p56 + "#!");
                        break;
                    case 57:
                        if (Pobj.p57 == null)
                        {
                            Pobj.p57 = "";
                        }
                        sb.Append(Pobj.p57 + "#!");
                        break;
                    case 58:
                        if (Pobj.p58 == null)
                        {
                            Pobj.p58 = "";
                        }
                        sb.Append(Pobj.p58 + "#!");
                        break;
                    case 59:
                        if (Pobj.p59 == null)
                        {
                            Pobj.p59 = "";
                        }
                        sb.Append(Pobj.p59 + "#!");
                        break;
                    case 60:
                        if (Pobj.p60 == null)
                        {
                            Pobj.p60 = "";
                        }
                        sb.Append(Pobj.p60 + "#!");
                        break;
                    case 61:
                        if (Pobj.p61 == null)
                        {
                            Pobj.p61 = "";
                        }
                        sb.Append(Pobj.p61 + "#!");
                        break;
                    case 62:
                        if (Pobj.p62 == null)
                        {
                            Pobj.p62 = "";
                        }
                        sb.Append(Pobj.p62 + "#!");
                        break;
                    case 63:
                        if (Pobj.p63 == null)
                        {
                            Pobj.p63 = "";
                        }
                        sb.Append(Pobj.p63 + "#!");
                        break;
                    case 64:
                        if (Pobj.p64 == null)
                        {
                            Pobj.p64 = "";
                        }
                        sb.Append(Pobj.p64 + "#!");
                        break;
                    case 65:
                        if (Pobj.p65 == null)
                        {
                            Pobj.p65 = "";
                        }
                        sb.Append(Pobj.p65 + "#!");
                        break;
                    case 66:
                        if (Pobj.p66 == null)
                        {
                            Pobj.p66 = "";
                        }
                        sb.Append(Pobj.p66 + "#!");
                        break;
                    case 67:
                        if (Pobj.p67 == null)
                        {
                            Pobj.p67 = "";
                        }
                        sb.Append(Pobj.p67 + "#!");
                        break;
                    case 68:
                        if (Pobj.p68 == null)
                        {
                            Pobj.p68 = "";
                        }
                        sb.Append(Pobj.p68 + "#!");
                        break;
                    case 69:
                        if (Pobj.p69 == null)
                        {
                            Pobj.p69 = "";
                        }
                        sb.Append(Pobj.p69 + "#!");
                        break;
                    case 70:
                        if (Pobj.p70 == null)
                        {
                            Pobj.p70 = "";
                        }
                        sb.Append(Pobj.p70 + "#!");
                        break;
                    case 71:
                        if (Pobj.p71 == null)
                        {
                            Pobj.p71 = "";
                        }
                        sb.Append(Pobj.p71 + "#!");
                        break;
                    case 72:
                        if (Pobj.p72 == null)
                        {
                            Pobj.p72 = "";
                        }
                        sb.Append(Pobj.p72 + "#!");
                        break;
                    case 73:
                        if
                            (Pobj.p73 == null)
                        {
                            Pobj.p73 = "";
                        }
                        sb.Append(Pobj.p73 + "#!");
                        break;
                    case 74:
                        if (Pobj.p74 == null)
                        {
                            Pobj.p74 = "";
                        }
                        sb.Append(Pobj.p74 + "#!");
                        break;
                    case 75:
                        if (Pobj.p75 == null)
                        {
                            Pobj.p75 = "";
                        }
                        sb.Append(Pobj.p75 + "#!");
                        break;
                    case 76:
                        if (Pobj.p76 == null)
                        {
                            Pobj.p76 = "";
                        }
                        sb.Append(Pobj.p76 + "#!");
                        break;
                    case 77:
                        if (Pobj.p77 == null)
                        {
                            Pobj.p77 = "";
                        }
                        sb.Append(Pobj.p77 + "#!");
                        break;
                    case 78:
                        if (Pobj.p78 == null)
                        {
                            Pobj.p78 = "";
                        }
                        sb.Append(Pobj.p78 + "#!");
                        break;
                    case 79:
                        if (Pobj.p79 == null)
                        {
                            Pobj.p79 = "";
                        }
                        sb.Append(Pobj.p79 + "#!");
                        break;
                    case 80:
                        if (Pobj.p80 == null)
                        {
                            Pobj.p80 = "";
                        }
                        sb.Append(Pobj.p80 + "#!");
                        break;
                    case 81:
                        if (Pobj.WCF入参1 == null)
                        {
                            Pobj.WCF入参1 = "";
                        }
                        sb.Append(Pobj.WCF入参1 + "#!");
                        break;
                    case 82:
                        if (Pobj.WCF入参2 == null)
                        {
                            Pobj.WCF入参2 = "";
                        }
                        sb.Append(Pobj.WCF入参2 + "#!");
                        break;
                    case 83:
                        if (Pobj.WCF入参3 == null)
                        {
                            Pobj.WCF入参3 = "";
                        }
                        sb.Append(Pobj.WCF入参3 + "#!");
                        break;
                    case 84:
                        sb.Append(Pobj.IsSuccess + "#!");
                        break;
                    case 85:
                        if (Pobj.ErrorMessage == null)
                        {
                            Pobj.ErrorMessage = "";
                        }
                        sb.Append(Pobj.ErrorMessage + "#!");
                        break;
                    case 86:
                        if (Pobj.WCF出参 == null)
                        {
                            Pobj.WCF出参 = "";
                        }
                        sb.Append(Pobj.WCF出参 + "#!");
                        break;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 字符串转换成参数对象
        /// </summary>
        /// <param name="Instr">字符串</param>
        /// <returns></returns>
        public static ParmObj StrToParmObj(string Instr)
        {
            ParmObj Pobj = new ParmObj();

            if (Instr.Length > 0)
            {
                string[] RowsObj = Instr.Split(new string[] { "#!" }, StringSplitOptions.None);
                for (int i = 0; i < RowsObj.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Pobj.p0 = RowsObj[i];
                            break;
                        case 1:
                            Pobj.p1 = RowsObj[i];
                            break;
                        case 2:
                            Pobj.p2 = RowsObj[i];
                            break;
                        case 3:
                            Pobj.p3 = RowsObj[i];
                            break;
                        case 4:
                            Pobj.p4 = RowsObj[i];
                            break;
                        case 5:
                            Pobj.p5 = RowsObj[i];
                            break;
                        case 6:
                            Pobj.p6 = RowsObj[i];
                            break;
                        case 7:
                            Pobj.p7 = RowsObj[i];
                            break;
                        case 8:
                            Pobj.p8 = RowsObj[i];
                            break;
                        case 9:
                            Pobj.p9 = RowsObj[i];
                            break;
                        case 10:
                            Pobj.p10 = RowsObj[i];
                            break;
                        case 11:
                            Pobj.p11 = RowsObj[i];
                            break;
                        case 12:
                            Pobj.p12 = RowsObj[i];
                            break;
                        case 13:
                            Pobj.p13 = RowsObj[i];
                            break;
                        case 14:
                            Pobj.p14 = RowsObj[i];
                            break;
                        case 15:
                            Pobj.p15 = RowsObj[i];
                            break;
                        case 16:
                            Pobj.p16 = RowsObj[i];
                            break;
                        case 17:
                            Pobj.p17 = RowsObj[i];
                            break;
                        case 18:
                            Pobj.p18 = RowsObj[i];
                            break;
                        case 19:
                            Pobj.p19 = RowsObj[i];
                            break;
                        case 20:
                            Pobj.p20 = RowsObj[i];
                            break;
                        case 21:
                            Pobj.p21 = RowsObj[i];
                            break;
                        case 22:
                            Pobj.p22 = RowsObj[i];
                            break;
                        case 23:
                            Pobj.p23 = RowsObj[i];
                            break;
                        case 24:
                            Pobj.p24 = RowsObj[i];
                            break;
                        case 25:
                            Pobj.p25 = RowsObj[i];
                            break;
                        case 26:
                            Pobj.p26 = RowsObj[i];
                            break;
                        case 27:
                            Pobj.p27 = RowsObj[i];
                            break;
                        case 28:
                            Pobj.p28 = RowsObj[i];
                            break;
                        case 29:
                            Pobj.p29 = RowsObj[i];
                            break;
                        case 30:
                            Pobj.p30 = RowsObj[i];
                            break;
                        case 31:
                            Pobj.p31 = RowsObj[i];
                            break;
                        case 32:
                            Pobj.p32 = RowsObj[i];
                            break;
                        case 33:
                            Pobj.p33 = RowsObj[i];
                            break;
                        case 34:
                            Pobj.p34 = RowsObj[i];
                            break;
                        case 35:
                            Pobj.p35 = RowsObj[i];
                            break;
                        case 36:
                            Pobj.p36 = RowsObj[i];
                            break;
                        case 37:
                            Pobj.p37 = RowsObj[i];
                            break;
                        case 38:
                            Pobj.p38 = RowsObj[i];
                            break;
                        case 39:
                            Pobj.p39 = RowsObj[i];
                            break;
                        case 40:
                            Pobj.p40 = RowsObj[i];
                            break;
                        case 41:
                            Pobj.p41 = RowsObj[i];
                            break;
                        case 42:
                            Pobj.p42 = RowsObj[i];
                            break;
                        case 43:
                            Pobj.p43 = RowsObj[i];
                            break;
                        case 44:
                            Pobj.p44 = RowsObj[i];
                            break;
                        case 45:
                            Pobj.p45 = RowsObj[i];
                            break;
                        case 46:
                            Pobj.p46 = RowsObj[i];
                            break;
                        case 47:
                            Pobj.p47 = RowsObj[i];
                            break;
                        case 48:
                            Pobj.p48 = RowsObj[i];
                            break;
                        case 49:
                            Pobj.p49 = RowsObj[i];
                            break;
                        case 50:
                            Pobj.p50 = RowsObj[i];
                            break;
                        case 51:
                            Pobj.p51 = RowsObj[i];
                            break;
                        case 52:
                            Pobj.p52 = RowsObj[i];
                            break;
                        case 53:
                            Pobj.p53 = RowsObj[i];
                            break;
                        case 54:
                            Pobj.p54 = RowsObj[i];
                            break;
                        case 55:
                            Pobj.p55 = RowsObj[i];
                            break;
                        case 56:
                            Pobj.p56 = RowsObj[i];
                            break;
                        case 57:
                            Pobj.p57 = RowsObj[i];
                            break;
                        case 58:
                            Pobj.p58 = RowsObj[i];
                            break;
                        case 59:
                            Pobj.p59 = RowsObj[i];
                            break;
                        case 60:
                            Pobj.p60 = RowsObj[i];
                            break;
                        case 61:
                            Pobj.p61 = RowsObj[i];
                            break;
                        case 62:
                            Pobj.p66 = RowsObj[i];
                            break;
                        case 63:
                            Pobj.p63 = RowsObj[i];
                            break;
                        case 64:
                            Pobj.p64 = RowsObj[i];
                            break;
                        case 65:
                            Pobj.p65 = RowsObj[i];
                            break;
                        case 66:
                            Pobj.p66 = RowsObj[i];
                            break;
                        case 67:
                            Pobj.p67 = RowsObj[i];
                            break;
                        case 68:
                            Pobj.p68 = RowsObj[i];
                            break;
                        case 69:
                            Pobj.p69 = RowsObj[i];
                            break;
                        case 70:
                            Pobj.p70 = RowsObj[i];
                            break;
                        case 71:
                            Pobj.p71 = RowsObj[i];
                            break;
                        case 72:
                            Pobj.p72 = RowsObj[i];
                            break;
                        case 73:
                            Pobj.p73 = RowsObj[i];
                            break;
                        case 74:
                            Pobj.p74 = RowsObj[i];
                            break;
                        case 75:
                            Pobj.p75 = RowsObj[i];
                            break;
                        case 76:
                            Pobj.p76 = RowsObj[i];
                            break;
                        case 77:
                            Pobj.p77 = RowsObj[i];
                            break;
                        case 78:
                            Pobj.p78 = RowsObj[i];
                            break;
                        case 79:
                            Pobj.p79 = RowsObj[i];
                            break;
                        case 80:
                            Pobj.p80 = RowsObj[i];
                            break;
                        case 81:
                            Pobj.WCF入参1 = RowsObj[i];
                            break;
                        case 82:
                            Pobj.WCF入参2 = RowsObj[i];
                            break;
                        case 83:
                            Pobj.WCF入参3 = RowsObj[i];
                            break;
                        case 84:
                            Pobj.IsSuccess = RowsObj[i].ToLower() == "true";
                            break;
                        case 85:
                            Pobj.ErrorMessage = RowsObj[i];
                            break;
                        case 86:
                            Pobj.WCF出参 = RowsObj[i];
                            break;
                    }
                }
                return Pobj;
            }
            else
            {
                return Pobj;
            }
        }
        /// <summary>
        /// 字符串转成二维数组
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns></returns>
        private static string[][] StrToArray(String Str)
        {


            try
            {
                if (!string.IsNullOrEmpty(Str))
                {

                    string[] RowsObj = Str.Split(new string[] { "~|" }, StringSplitOptions.None);

                    string[][] RtnArray = new string[RowsObj.Length - 1][];


                    for (int l = 0; l < RowsObj.Length - 1; l++)
                    {
                        string[] TmpArr = RowsObj[l].Split(new string[] { "~" }, StringSplitOptions.None);

                        string[] RowArr = new string[TmpArr.Length - 1];

                        for (int i = 0; i < RowArr.Length; i++)
                        {
                            RowArr[i] = TmpArr[i];
                        }

                        RtnArray[l] = RowArr;
                    }
                    return RtnArray;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 字符串转成泛型集合
        /// </summary>
        /// <typeparam name="T">泛型集合</typeparam>
        /// <param name="StrCol">列名字符串</param>
        /// <param name="StrVal">值字符串</param>
        /// <returns></returns>
        public static List<T> StrToList<T>(string StrCol, string StrVal) where T : new()
        {
            List<T> tList = new List<T>();
            string[] Val = StrVal.Split(new string[] { "~|" }, StringSplitOptions.None);
            string[] Col = StrCol.Split(',');

            for (int i = 0; i < Val.Length - 1; i++)
            {
                string[] v = Val[i].Split('~');
                Dictionary<string, string> temp = new Dictionary<string, string>();

                for (int j = 0; j < Col.Length; j++)
                {
                    temp.Add(Col[j], v[j]);
                }
                tList.Add(SetListVaue<T>(temp));
            }
            return tList;
        }
        /// <summary>
        /// 泛型转字符串    
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="list">泛型对像</param>
        /// <returns>字符串</returns>
        public static string ListToStr<T>(List<T> list, string StrCol, bool ColAll)
        {
            StringBuilder str_数据字符串 = new StringBuilder();
            string[] strCols = StrCol.TrimEnd(',').Split(',');

            Type ty = typeof(T);
            PropertyInfo[] Propers = ty.GetProperties();


            if (ColAll == true)
            {
                //取属性名
                for (int i = 0; i < Propers.Length; i++)
                {
                    str_数据字符串.Append(Propers[i].Name + ",");
                }
                str_数据字符串.Remove(str_数据字符串.Length - 1, 1);
                str_数据字符串.Append("##");
            }
            else
            {
                str_数据字符串.Append(StrCol.TrimEnd(',') + "##");
            }
            //取属性值
            for (int i = 0; i < list.Count; i++)
            {
                foreach (PropertyInfo p in Propers)
                {
                    object obj = p.GetValue(list[i], null);
                    if (ColAll == false)
                    {
                        foreach (string strCol in strCols)
                        {
                            if (strCol.Trim() == p.Name)
                            {
                                if (obj == null)
                                {
                                    str_数据字符串.Append("" + "~");
                                }
                                else
                                {
                                    str_数据字符串.Append(obj.ToString() + "~");
                                }
                            }
                        }
                    }
                    else
                    {

                        if (obj == null)
                        {
                            str_数据字符串.Append("" + "~");
                        }
                        else
                        {
                            str_数据字符串.Append(obj.ToString() + "~");
                        }

                    }
                }
                str_数据字符串.Append("|");
            }

            return str_数据字符串.ToString();

        }

        private static T SetListVaue<T>(Dictionary<string, string> values) where T : new()
        {
            T result = new T();
            Type type = typeof(T);
            PropertyInfo[] pi = type.GetProperties();
            foreach (var item in pi)
            {
                Type proType = item.PropertyType;
                if (!values.ContainsKey(item.Name))
                {
                    continue;
                }
                object value = values[item.Name];
                //这里的IF也可以改用SWITCH来判断。
                if (proType == typeof(String))
                {
                    item.SetValue(result, value, null);
                }
                else if (proType == typeof(Int32))
                {
                    item.SetValue(result, item == null ? 0 : Convert.ToInt32(value), null);
                }
                else if (proType == typeof(Nullable<int>))//int?
                {
                    //所有Nulable<T>的类型以此类推，如double?类型
                    item.SetValue(result, values[item.Name] == null ? null : (int?)Convert.ToInt32(value), null);
                }
                else if (proType == typeof(Double))
                {
                    item.SetValue(result, item == null || value.ToString().EndsWith("") ? null : (Double?)Convert.ToDouble(value), null);
                }
                else if (proType == typeof(DateTime))
                {
                    item.SetValue(result, item == null || value.ToString().EndsWith("") ? null : (DateTime?)Convert.ToDateTime(value), null);
                }
                else if (proType == typeof(Decimal))
                {
                    item.SetValue(result, item == null || value.ToString().EndsWith("") ? null : (Decimal?)Convert.ToDecimal(value), null);
                }
                else
                {
                    //继续用if或者switch/case添加更多的可能。以应对更复杂的自定义类型
                }
            }

            return result;
        }
        /// <summary>
        /// 字符串转成DataTable,其中的列是指定的
        /// </summary>
        /// <param name="DataType">实体DataTable</param>
        /// <param name="StrCol">字符串插入的列名</param>
        /// <param name="StrVal">字符串的值</param>
        /// <returns></returns>
        public static DataTable StrToDataTable(DataTable DataType, string StrCol, string StrVal)
        {
            string[] Cols = StrCol.Split(',');
            string[] Vals = StrVal.Split(new string[] { "~|" }, StringSplitOptions.None);
            DataTable dt_temptable = null;
            if (DataType != null)
            {
                dt_temptable = DataType.Clone();
            }
            else//如果没有传带结构的数据集，则自行创建一个
            {
                dt_temptable = new DataTable();
                for (int i = 0; i < Cols.Length; i++)
                {
                    dt_temptable.Columns.Add(Cols[i], typeof(String));
                }
            }

            Dictionary<string, Type> colType = new Dictionary<string, Type>();

            for (int j = 0; j < dt_temptable.Columns.Count; j++)
            {
                colType.Add(dt_temptable.Columns[j].ColumnName.ToLower(), dt_temptable.Columns[j].DataType);
            }


            object falg;
            for (int i = 0; i < Vals.Length - 1; i++)
            {
                DataRow dr = dt_temptable.NewRow();
                string[] v = Vals[i].Split('~');

                for (int j = 0; j < v.Length; j++)
                {
                    try
                    {
                        if (v[j].Length > 0)
                        {
                            string str_colName = Cols[j].ToLower();
                            if (!dt_temptable.Columns.Contains(str_colName))
                                continue;
                            Type t_colType = colType[str_colName];
                            if (t_colType == typeof(int))
                                falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = int.Parse(v[j]);
                            else if (t_colType == typeof(String))
                                dr[Cols[j]] = v[j].Replace("～～～", "~");
                            else if (t_colType == typeof(Double))
                                falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = double.Parse(v[j]);
                            else if (t_colType == typeof(DateTime))
                                falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = DateTime.Parse((v[j]));
                            else if (t_colType == typeof(Decimal))
                                falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = decimal.Parse((v[j]));
                        }
                    }
                    catch
                    {
                        string aa = "报错误";
                    }
                }
                dt_temptable.Rows.Add(dr.ItemArray);
            }
            return dt_temptable;
        }
        /// <summary>
        /// 字符串转成DataTable,其中的列是指定的。字符串的格式: 列名字符串##值字符串
        /// </summary>
        /// <param name="dataType">实体DataTable</param>
        /// <param name="strAll">列名和值的字符串</param>
        /// <returns></returns>
        public static DataTable StrToDataTable(DataTable dataType, string strAll)
        {
            try
            {
                string StrVal = strAll.Split(new string[] { "##" }, StringSplitOptions.None)[1];
                string StrCol = strAll.Split(new string[] { "##" }, StringSplitOptions.None)[0];
                return StrToDataTable(dataType, StrCol, StrVal);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// DataTable转成字符串。字符串的格式: 列名字符串##值字符串
        /// </summary>
        /// <param name="Dt">实体DataTable</param>
        /// <param name="StrCol">转换列表字符串</param>
        /// <param name="ColAll">是否为全部列</param>
        /// <returns></returns>
        public static string DataTableToStr(DataTable Dt, string StrCol, bool ColAll)
        {
            StringBuilder str = new StringBuilder();


            String[] Col = StrCol.Split(',');

            for (int i = 0; i < Col.Length; i++)
            {
                Col[i] = Col[i].Trim();
            }

            if (ColAll)
            {
                foreach (DataColumn dc in Dt.Columns)
                {
                    str.Append(dc.ColumnName + ",");
                }

                str = str.Remove(str.Length - 1, 1);
                str.Append("##");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {
                        str.Append(Dt.Rows[i][j].ToString().Replace("~", "～～～") + "~");
                    }
                    str.Append("~|");
                }

            }
            else
            {
                str.Append(StrCol + "##");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    for (int j = 0; j < Col.Length; j++)
                    {
                        str.Append(Dt.Rows[i][Col[j]].ToString().Replace("~", "～～～") + "~");
                    }
                    str.Append("~|");
                }

            }

            return str.ToString();
        }
        /// <summary>
        /// 数据流转成DataTable。字符串的格式: DataTable架构@@DataTable值字符串
        /// </summary>
        /// <param name="InStream">数据流</param>
        /// <param name="DataAll">真则导入架构和数据，假则只导入架构</param>
        /// <returns></returns>
        public static DataTable StreamToDataTable(Stream InStream, bool DataAll)
        {
            string Str_结构 = "", Str_数据 = "";

            DataTable dt_返回值 = new DataTable();
            ZipHelper Zip = new ZipHelper();

            //解压
            string Str_字符流 = Zip.GZipDecompress(InStream);


            string[] ArrStr = Str_字符流.Split(new string[] { "@@" }, StringSplitOptions.None);

            if (ArrStr.Length <= 0)
            {
                return dt_返回值;
            }

            if (ArrStr.Length == 1)
            {
                Str_结构 = ArrStr[0];
            }
            if (ArrStr.Length == 2)
            {
                Str_结构 = ArrStr[0];
                Str_数据 = ArrStr[1];
            }

            if (ArrStr.Length == 3)
            {
                Str_结构 = ArrStr[0];
                Str_数据 = ArrStr[1];
            }



            //导入架构和数据;
            if (DataAll)
            {

                StringReader tr = new StringReader(Str_结构);
                //导入架构
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(tr);
                dt_返回值 = ds.Tables[0];

                //导入数据
                if (!System.String.IsNullOrEmpty(Str_数据))
                {
                    dt_返回值 = StrToDataTable(dt_返回值, Str_数据);
                }
            }
            else
            {
                //只导入架构
                StringReader tr = new StringReader(Str_结构);
                //导入架构
                dt_返回值.ReadXmlSchema(tr);
            }
            return dt_返回值;
        }
        /// <summary>
        /// 数据流转成DataSet。字符串的格式: DataSet架构@@DataTable1值字符串@@DataTable2值字符串
        /// </summary>
        /// <param name="InStream">数据流</param>
        /// <param name="DataAll">真则导入架构和数据，假则只导入架构</param>
        /// <returns></returns>
        public static DataSet StreamToDataSet(Stream InStream, bool DataAll)
        {

            DataSet ds_临时 = new DataSet();
            DataSet ds_返回值 = new DataSet();
            ZipHelper Zip = new ZipHelper();

            //解压
            string Str_字符流 = Zip.GZipDecompress(InStream);


            string[] ArrStr = Str_字符流.Split(new string[] { "@@" }, StringSplitOptions.None);

            if (ArrStr.Length <= 0)
            {
                return ds_返回值;
            }

            //导入架构和数据;
            if (DataAll)
            {

                StringReader tr = new StringReader(ArrStr[0]);
                //导入架构
                ds_临时.ReadXmlSchema(tr);

                //导入数据
                for (int i = 0; i < ds_临时.Tables.Count; i++)
                {
                    DataTable dt_临时 = StrToDataTable(ds_临时.Tables[i], ArrStr[i + 1]);
                    ds_返回值.Tables.Add(dt_临时.Copy());

                }

            }
            else
            {
                //只导入架构
                StringReader tr = new StringReader(ArrStr[0]);
                //导入架构
                ds_返回值.ReadXmlSchema(tr);
            }
            return ds_返回值;
        }
        /// <summary>
        /// 把字符串写到指定的文件中。
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="StrVal"></param>
        public static bool StrToFile(String FilePath, string StrVal)
        {
            try
            {
                ///实例化一个文件流--->与写入文件相关联  
                FileStream fs = new FileStream(FilePath, FileMode.Create);
                //实例化一个StreamWriter-->与fs相关联  
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                //开始写入  
                sw.Write(StrVal);  //清空缓冲区  
                sw.Flush();
                //关闭流  
                sw.Close();
                fs.Close();
                return File.Exists(FilePath);

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }






        public static DataSet StreamToDataSet(Stream InStream, bool DataAll, string str)
        {

            DataSet ds_临时 = new DataSet();
            DataSet ds_返回值 = new DataSet();
            ZipHelper Zip = new ZipHelper();

            //解压
            string Str_字符流 = Zip.GZipDecompress(InStream);


            string[] ArrStr = Str_字符流.Split(new string[] { "@@" }, StringSplitOptions.None);

            if (ArrStr.Length <= 0)
            {
                return ds_返回值;
            }

            //导入架构和数据;
            if (DataAll)
            {

                StringReader tr = new StringReader(ArrStr[0]);
                //导入架构
                ds_临时.ReadXmlSchema(tr);

                //导入数据
                for (int i = 0; i < ds_临时.Tables.Count; i++)
                {
                    DataTable dt_临时 = StrToDataTable(ds_临时.Tables[i], ArrStr[i + 1], true);
                    ds_返回值.Tables.Add(dt_临时.Copy());

                }

            }
            else
            {
                //只导入架构
                StringReader tr = new StringReader(ArrStr[0]);
                //导入架构
                ds_返回值.ReadXmlSchema(tr);
            }
            return ds_返回值;
        }

        public static string DataTableToStr(DataTable Dt, string StrCol, bool ColAll, string st)
        {
            StringBuilder str = new StringBuilder();


            String[] Col = StrCol.Split(',');

            for (int i = 0; i < Col.Length; i++)
            {
                Col[i] = Col[i].Trim();
            }

            if (ColAll)
            {
                foreach (DataColumn dc in Dt.Columns)
                {
                    str.Append(dc.ColumnName + ",");
                }

                str = str.Remove(str.Length - 1, 1);
                str.Append("##");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    for (int j = 0; j < Dt.Columns.Count; j++)
                    {
                        str.Append(Dt.Rows[i][j].ToString().Replace("~", "～～～") + "~");
                    }
                    str.Append("~|");
                }

            }
            else
            {
                str.Append(StrCol + "##");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    for (int j = 0; j < Col.Length; j++)
                    {
                        str.Append(Dt.Rows[i][Col[j]].ToString() + "~");
                    }
                    str.Append("~|");
                }

            }

            return str.ToString();
        }

        public static DataTable StrToDataTable(DataTable dataType, string strAll, bool bl)
        {
            try
            {
                string StrVal = strAll.Split(new string[] { "##" }, StringSplitOptions.None)[1];
                string StrCol = strAll.Split(new string[] { "##" }, StringSplitOptions.None)[0];
                return StrToDataTable(dataType, StrCol, StrVal, true);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable StrToDataTable(DataTable DataType, string StrCol, string StrVa, bool bl)
        {
            string[] Cols = StrCol.Split(',');
            string[] Vals = StrVa.Split(new string[] { "~|" }, StringSplitOptions.None);
            DataTable dt_temptable = null;
            if (DataType != null)
            {
                dt_temptable = DataType.Clone();
            }
            else//如果没有传带结构的数据集，则自行创建一个
            {
                dt_temptable = new DataTable();
                for (int i = 0; i < Cols.Length; i++)
                {
                    dt_temptable.Columns.Add(Cols[i], typeof(String));
                }
            }

            Dictionary<string, Type> colType = new Dictionary<string, Type>();

            for (int j = 0; j < dt_temptable.Columns.Count; j++)
            {
                colType.Add(dt_temptable.Columns[j].ColumnName, dt_temptable.Columns[j].DataType);
            }


            object falg;
            for (int i = 0; i < Vals.Length - 1; i++)
            {
                DataRow dr = dt_temptable.NewRow();
                string[] v = Vals[i].Split(new string[] { "~" }, StringSplitOptions.None);

                for (int j = 0; j < v.Length; j++)
                {
                    if (v[j].Length > 0)
                    {
                        string str_colName = Cols[j];
                        Type t_colType = colType[str_colName];
                        if (t_colType == typeof(int))
                            falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = int.Parse(v[j]);
                        else if (t_colType == typeof(String))
                            dr[Cols[j]] = v[j].Replace("～～～", "~");
                        else if (t_colType == typeof(Double))
                            falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = double.Parse(v[j]);
                        else if (t_colType == typeof(DateTime))
                            falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = DateTime.Parse((v[j]));
                        else if (t_colType == typeof(Decimal))
                            falg = string.IsNullOrEmpty(v[j]) ? dr[Cols[j]] = DBNull.Value : dr[Cols[j]] = decimal.Parse((v[j]));
                    }
                }
                dt_temptable.Rows.Add(dr.ItemArray);
            }
            return dt_temptable;
        }


        public static DataTable ListToDataTable<T>(ref DataTable dataTable, List<T> list)
        {
            if (dataTable.Columns.Count == 0)
            {
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    DataColumn col = new DataColumn(info.Name);
                    //col.DataType = info.GetType();
                    dataTable.Columns.Add(col);
                }
            }

            foreach (T t in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static DataTable EntityToDataTable<T>(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 合并两个Table中的列
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable dt = new DataTable();
            //向dt中添加dt1的列名     
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                if (dt2.Columns.Contains(dt1.Columns[i].ColumnName.ToString()))
                {
                    dt2.Columns.Remove(dt1.Columns[i].ColumnName.ToString());
                }

                dt.Columns.Add(dt1.Columns[i].ColumnName.ToString());
            }
            //向dt中添加dt2的列名   
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt.Columns.Add(dt2.Columns[i].ColumnName.ToString());
            }
            return dt.Copy();
        }

        public static string DictionaryToString(Dictionary<int, string> dic)
        {
            string str = "";
            foreach (int key in dic.Keys)
            {
                str += key + "θ" + dic[key] + "ð";
            }
            if (!string.IsNullOrEmpty(str))
                str = str.Substring(0, str.Length - 1);

            return str;
        }

        public static Dictionary<int, string> StringToDictionary(string strValue)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string[] arr = strValue.Split('ð');

            foreach (string str in arr)
            {
                int key = int.Parse(str.Split('θ')[0]);
                string value = str.Split('θ')[1];

                dic.Add(key, value);
            }

            return dic;
        }
    }

}
