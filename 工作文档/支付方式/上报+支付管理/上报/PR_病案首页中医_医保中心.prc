CREATE OR REPLACE PROCEDURE PR_������ҳ��ҽ_ҽ������(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_SQL VARCHAR2(1000);

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);

  --��ȡ��ϼ�¼CURSOR
  CURSOR CUR_��ϼ�¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������, T.��Ϸ���, T.������� ORDER BY T.�������, T.�Ƿ������ DESC) RN,
                   T.סԺ������,
                   T.��Ϸ���,
                   T.�������,
                   (CASE
                     WHEN T.ICD�� = T.�������� THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������)
                     ELSE
                      T.ICD��
                   END) ICD��,
                   T.��������,
                   T.��Ժ����,
                   
                   (CASE
                     WHEN T.ICD��1 = T.��������1 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������1)
                     ELSE
                      T.ICD��1
                   END) ICD��1,
                   T.��������1,
                   T.��Ժ����1,
                   
                   (CASE
                     WHEN T.ICD��2 = T.��������2 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������2)
                     ELSE
                      T.ICD��2
                   END) ICD��2,
                   T.��������2,
                   T.��Ժ����2,
                   
                   (CASE
                     WHEN T.ICD��3 = T.��������3 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������3)
                     ELSE
                      T.ICD��3
                   END) ICD��3,
                   T.��������3,
                   T.��Ժ����3,
                   
                   (CASE
                     WHEN T.ICD��4 = T.��������4 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������4)
                     ELSE
                      T.ICD��4
                   END) ICD��4,
                   T.��������4,
                   T.��Ժ����4,
                   
                   (CASE
                     WHEN T.ICD��5 = T.��������5 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������5)
                     ELSE
                      T.ICD��5
                   END) ICD��5,
                   T.��������5,
                   T.��Ժ����5,
                   
                   (CASE
                     WHEN T.ICD��6 = T.��������6 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������6)
                     ELSE
                      T.ICD��6
                   END) ICD��6,
                   T.��������6,
                   T.��Ժ����6,
                   
                   (CASE
                     WHEN T.ICD��7 = T.��������7 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������7)
                     ELSE
                      T.ICD��7
                   END) ICD��7,
                   T.��������7,
                   T.��Ժ����7,
                   
                   (CASE
                     WHEN T.ICD��8 = T.��������8 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������8)
                     ELSE
                      T.ICD��8
                   END) ICD��8,
                   T.��������8,
                   T.��Ժ����8,
                   
                   (CASE
                     WHEN T.ICD��9 = T.��������9 THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������9)
                     ELSE
                      T.ICD��9
                   END) ICD��9,
                   T.��������9,
                   T.��Ժ����9
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TT.�������ͱ��� = '2' --ҽ��
               AND TTT.������� != '3' --��ҽ
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND ((T.��Ϸ��� = '2' and T.������� = '��Ժ���') or
                   (T.��Ϸ��� = '1' and
                   T.������� IN ('��Ժ���',
                                '�������',
                                '���˺��ж��ⲿԭ��',
                                '�������')))
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE (G.������� = '�������' AND G.RN <= 15)
        OR G.������� <> '�������';
  ROW_��ϼ�¼ CUR_��ϼ�¼%ROWTYPE;

  --��ȡ������¼CURSOR
  CURSOR CUR_������¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.סԺ������,
                   T.������,
                   TO_CHAR(T.������������, 'yyyy-MM-dd') ������������,
                   T.�����������,
                   T.������������,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����) AS ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.I��) AS I��,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.II��) AS II��,
                   DECODE(FU_ͨ��_��ȡ�ַ���ֵ(T.�п����ϵȼ�, '/', 1),
                          '��',
                          '2',
                          '��',
                          '3',
                          '��',
                          '4',
                          '') �пڵȼ�,
                   DECODE(FU_ͨ��_��ȡ�ַ���ֵ(T.�п����ϵȼ�, '/', 2),
                          '��',
                          '1',
                          '��',
                          '2',
                          '��',
                          '3',
                          '9',
                          '') �п����ϵȼ�,
                   T.����ʽ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����ҽʦ) AS ����ҽʦ
              FROM סԺ����_������ҳ������ T,
                   סԺ����_��Ժ������Ϣ   TT,
                   סԺ����_������ҳ       TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TT.�������ͱ��� = '2'
               AND TTT.������� != '3' --��ҽ
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN <= 7;

  ROW_������¼ CUR_������¼%ROWTYPE;

  --��ȡ���ü�¼CURSOR
  CURSOR CUR_���ü�¼ IS
    SELECT T.��������,
           T.סԺ������,
           SUM(T.������) AS �ܽ��,
           (SELECT (A.ʵ�ս�� - A.�ܲ������)
              FROM סԺ����_��Ժ���˷�Ʊ�Ǽ� A
             WHERE A.�������� = STR_��������
               AND A.סԺ������ = T.סԺ������
               AND A.�ٻر�־ = '��') AS �Ը����,
           nvl(SUM(CASE T.�������
                     WHEN '10029' THEN
                      T.������
                     WHEN '10030' THEN
                      T.������
                     WHEN '10031' THEN
                      T.������
                   END),
               0) AS һ��ҽ�Ʒ����,
           nvl(SUM(CASE T.�������
                     WHEN '10030' THEN
                      T.������
                   END),
               0) AS ����_��ҽ��֤���η�,
           nvl(SUM(CASE T.�������
                     WHEN '10031' THEN
                      T.������
                   END),
               0) AS ����_��ҽ��֤���λ����,
           nvl(SUM(CASE T.�������
                     WHEN '10032' THEN
                      T.������
                   END),
               0) AS һ�����Ʋ�����,
           nvl(SUM(CASE T.�������
                     WHEN '10033' THEN
                      T.������
                   END),
               0) AS �����,
           nvl(SUM(CASE T.�������
                     WHEN '10034' THEN
                      T.������
                   END),
               0) AS ��������,
           nvl(SUM(CASE T.�������
                     WHEN '10035' THEN
                      T.������
                   END),
               0) AS ������Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10036' THEN
                      T.������
                   END),
               0) AS ʵ������Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10037' THEN
                      T.������
                   END),
               0) AS Ӱ��ѧ��Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10038' THEN
                      T.������
                   END),
               0) AS �ٴ������Ŀ��,
           nvl(SUM(CASE T.�������
                     WHEN '10039' THEN
                      T.������
                     WHEN '10040' THEN
                      T.������
                   END),
               0) AS ������������Ŀ��,
           nvl(SUM(CASE T.�������
                     WHEN '10040' THEN
                      T.������
                   END),
               0) AS ����_�ٴ��������Ʒ�,
           nvl(SUM(CASE T.�������
                     WHEN '10041' THEN
                      T.������
                     WHEN '10042' THEN
                      T.������
                     WHEN '10043' THEN
                      T.������
                   END),
               0) AS �������Ʒ�,
           nvl(SUM(CASE T.�������
                     WHEN '10042' THEN
                      T.������
                   END),
               0) AS ����_�����,
           nvl(SUM(CASE T.�������
                     WHEN '10043' THEN
                      T.������
                   END),
               0) AS ����_������,
           nvl(SUM(CASE T.�������
                     WHEN '10044' THEN
                      T.������
                   END),
               0) AS ������,
           nvl(SUM(CASE T.�������
                     WHEN '10045' THEN
                      T.������
                   END),
               0) AS ��ҽ���,
           nvl(SUM(CASE T.�������
                     WHEN '10046' THEN
                      T.������
                     WHEN '10047' THEN
                      T.������
                     WHEN '10048' THEN
                      T.������
                     WHEN '10049' THEN
                      T.������
                     WHEN '10050' THEN
                      T.������
                     WHEN '10051' THEN
                      T.������
                     WHEN '10052' THEN
                      T.������
                   END),
               0) AS ��ҽ����,
           nvl(SUM(CASE T.�������
                     WHEN '10047' THEN
                      T.������
                   END),
               0) AS ����_��ҽ����,
           nvl(SUM(CASE T.�������
                     WHEN '10048' THEN
                      T.������
                   END),
               0) AS ����_��ҽ����,
           nvl(SUM(CASE T.�������
                     WHEN '10049' THEN
                      T.������
                   END),
               0) AS ����_�����ķ�,
           nvl(SUM(CASE T.�������
                     WHEN '10050' THEN
                      T.������
                   END),
               0) AS ����_��ҽ��������,
           nvl(SUM(CASE T.�������
                     WHEN '10051' THEN
                      T.������
                   END),
               0) AS ����_��ҽ�س�����,
           nvl(SUM(CASE T.�������
                     WHEN '10052' THEN
                      T.������
                   END),
               0) AS ����_��ҽ��������,
           nvl(SUM(CASE T.�������
                     WHEN '10053' THEN
                      T.������
                     WHEN '10054' THEN
                      T.������
                     WHEN '10055' THEN
                      T.������
                   END),
               0) AS ��ҽ����,
           nvl(SUM(CASE T.�������
                     WHEN '10054' THEN
                      T.������
                   END),
               0) AS ����_��ҽ�������ӹ�,
           nvl(SUM(CASE T.�������
                     WHEN '10055' THEN
                      T.������
                   END),
               0) AS ����_��֤ʩ��,
           nvl(SUM(CASE T.�������
                     WHEN '10056' THEN
                      T.������
                     WHEN '10057' THEN
                      T.������
                   END),
               0) AS ��ҩ��,
           nvl(SUM(CASE T.�������
                     WHEN '10057' THEN
                      T.������
                   END),
               0) AS ����_����ҩ���,
           nvl(SUM(CASE T.�������
                     WHEN '10058' THEN
                      T.������
                     WHEN '10059' THEN
                      T.������
                   END),
               0) AS �г�ҩ��,
           nvl(SUM(CASE T.�������
                     WHEN '10059' THEN
                      T.������
                   END),
               0) AS ����_ҽ�ƻ�����ҩ�Ƽ���,
           nvl(SUM(CASE T.�������
                     WHEN '10060' THEN
                      T.������
                   END),
               0) AS �в�ҩ��,
           nvl(SUM(CASE T.�������
                     WHEN '10061' THEN
                      T.������
                   END),
               0) AS Ѫ��,
           nvl(SUM(CASE T.�������
                     WHEN '10062' THEN
                      T.������
                   END),
               0) AS �׵�������Ʒ��,
           nvl(SUM(CASE T.�������
                     WHEN '10063' THEN
                      T.������
                   END),
               0) AS �򵰰�����Ʒ��,
           nvl(SUM(CASE T.�������
                     WHEN '10064' THEN
                      T.������
                   END),
               0) AS ��Ѫ��������Ʒ��,
           nvl(SUM(CASE T.�������
                     WHEN '10065' THEN
                      T.������
                   END),
               0) AS ϸ����������Ʒ��,
           nvl(SUM(CASE T.�������
                     WHEN '10066' THEN
                      T.������
                   END),
               0) AS �����һ����ҽ�ò��Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10067' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10068' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           nvl(SUM(CASE T.�������
                     WHEN '10069' THEN
                      T.������
                   END),
               0) AS ������
    
      FROM (SELECT ZT.����       AS �������,
                   ZT.����       AS ��������,
                   ZZ.�ܽ��     AS ������,
                   ZZ.��������,
                   ZZ.סԺ������,
                   ZZ.����ʱ��
              FROM ������Ŀ_�ֵ���ϸ ZT
              LEFT JOIN (SELECT SUM(�ܽ��) AS �ܽ��,
                               ��ҽ��������,
                               ��������,
                               סԺ������,
                               ����ʱ��
                          FROM (SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Z.BAGLBM1, '10069') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_ZLZD_FJXM Z
                                    ON A.�������� = Z.JGBM
                                   AND A.��Ŀ���� = Z.XMBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '1'
                                
                                UNION ALL
                                
                                SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Y.BAGLBM1, '10069') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_YPZD_FJSX Y
                                    ON A.�������� = Y.JGBM
                                   AND A.��Ŀ���� = Y.YPBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '2')
                         GROUP BY ��������,
                                  סԺ������,
                                  ����ʱ��,
                                  ��ҽ��������) ZZ
                ON ZT.���� = ZZ.��ҽ��������
             WHERE ZT.������� = 'GB_009001'
               AND ZT.��Ч״̬ = '��Ч'
               AND ZT.ɾ����־ = '0'
               AND ZT.���� LIKE '��ҽ����_%') T,
           סԺ����_��Ժ������Ϣ TT,
           סԺ����_������ҳ TTT
     WHERE T.סԺ������ = TT.סԺ������
       AND T.סԺ������ = TTT.סԺ������
       AND TT.�������ͱ��� = '2'
       and TTT.������� != '3'
       AND TTT.�鵵�˱��� IS NOT NULL
       AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
       AND T.����ʱ�� >= TT.��Ժʱ��
       AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
           TT.������ LIKE '%' || STR_�������� || '%' OR
           TT.�������� LIKE '%' || STR_�������� || '%')
     GROUP BY T.��������, T.סԺ������;

  ROW_���ü�¼ CUR_���ü�¼%ROWTYPE;

BEGIN

  BEGIN
    --���»�����Ϣ
    INSERT INTO ��ʱ��_������ҳ��ҽ_ҽ������
      (JYH, --��ҽ�ţ�ҽԺ�ڲ����ɵ�Ψһ��ţ�
       AAZ107, --ҽ��ҽ�ƻ�������
       IDC, --��֯��������
       YLFKFS, --ҽ�Ƹ��ѷ�ʽ
       JKKH, --��������
       ZYCS, --סԺ����
       BAH, --������
       XM, --����
       XB, --�Ա� 1.�� 2.Ů
       CSRQ, --��������
       NL, --���� 
       GJ, --����
       BZYZS_NL, --(���䲻��һ�����)����
       XSETZ, --��������������
       XSERYTZ, --��������Ժ����
       CSD, --������
       GG, --����
       MZ, --����
       SFZH, --���֤��
       ZY, --ְҵ
       HY, --���� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����
       XZZ, --��סַ
       DH, --�绰(��סַ)
       YB1, --�ʱ�(��סַ)
       HKDZ, --���ڵ�ַ
       YB2, --�ʱ�(���ڵ�ַ)
       GZDWJDZ, --������λ����ַ
       DWDH, --�����绰(������λ����ַ)
       YB3, --�ʱ�(������λ����ַ)
       LXRXM, --��ϵ������
       GX, --��ϵ�˹�ϵ
       DZ, --��ϵ�˵�ַ
       DH1, --��ϵ�˵绰
       RYTJ, --��Ժ;�� 1.����  2.����  3.����ҽ�ƻ���ת��  9.����
       ZLLB, --�������
       RYSJ, --��Ժʱ��
       RYSJ_S, --��Ժʱ��(ʱ)
       RYKB, --��Ժ�Ʊ�
       RYBF, --��Ժ����
       ZKKB, --ת�ƿƱ�
       CYSJ, --��Ժʱ��
       CYSJ_S, --��Ժʱ��(ʱ)
       CYKB, --��Ժ�Ʊ�
       CYBF, --��Ժ����
       SJZY, --ʵ��סԺ����
       MZD_ZYZD, --��(��)���������1
       JBDM_S, --��(��)�����1
       MZZD_XYZD, --��(��)���������2
       JBBM_S, --��(��)�����2
       SSLCLJ, --ʵʩ�ٴ�·��
       ZYYJ, --ʹ��ҽ�ƻ�����ҩ�Ƽ�
       ZYZLSB, --ʹ����ҽ�����豸
       ZYZLJS, --ʹ����ҽ���Ƽ���
       BZSH, --��֤ʩ��
       
       --ʡ����ϼ�¼
       
       BLH, --�����
       YWGM, --�Ƿ�ҩ�����
       GMYW, --����ҩ��
       SJ, --��������ʬ��
       XX, --Ѫ��
       RH, --RH
       QJCS, --���ȴ���
       CGCS, --�ɹ�����
       SXFY, --��Ѫ��Ӧ
       RCMDSC, --����÷��ɸ��
       XSRJBSC, --����������ɸ��
       CHCX, --�����Ѫ
       KZR, --������
       ZRYS, --����(������)ҽʦ
       ZZYS, --����ҽʦ
       ZYYS, --סԺҽʦ
       ZRHS, --���λ�ʿ
       JXYS, --����ҽʦ
       SXYS, --ʵϰҽʦ
       BMY, --����Ա
       BAZL, --��������
       ZKYS, --�ʿ�ҽʦ
       ZKHS, --�ʿػ�ʿ
       ZKRQ, --�ʿ�����
       
       --ʡ��������¼
       
       LYFS, --��Ժ��ʽ
       ZZYJH, --��סԺ�ƻ�
       MD, --Ŀ��
       RYQ_T, --��Ժǰ��
       RYQ_XS, --��ԺǰСʱ
       RYQ_F, --��Ժǰ����
       RYH_T, --��Ժ����
       RYH_XS, --��Ժ��Сʱ
       RYH_F --��Ժ�����
       
       --ʡ��������¼
       
       )
      SELECT T.סԺ������,
             '' ҽ��ҽ�ƻ�������,
             '122108044641640037' AS ��֯��������,
             T.ҽ�Ƹ��ѷ�ʽ,
             T.��������,
             T.סԺ����,
             T.������,
             SUBSTR(T.��������, 0, 20),
             NVL(T.�Ա�, '0'),
             TO_CHAR(T.��������, 'yyyy-MM-dd') ��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             T.����,
             TO_NUMBER(REGEXP_REPLACE(T.���䲻��1����, '[^-0-9.]', '')) ���䲻��1����,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������������, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������������,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������Ժ����, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������Ժ����,
             ((SELECT ����
                 FROM ������Ŀ_�ֵ���ϸ
                WHERE ������� = 'RC036'
                  AND ɾ����־ = '0'
                  AND ���� = T.������ʡ) || T.�������� || T.��������) AS ������,
             ((SELECT ����
                 FROM ������Ŀ_�ֵ���ϸ
                WHERE ������� = 'RC036'
                  AND ɾ����־ = '0'
                  AND ���� = T.����ʡ) || T.������ || T.��������) AS ����,
             T.����,
             T.���֤��,
             T.ְҵ,
             T.����,
             T.��סַ,
             SUBSTR(T.���ڵ绰, 0, 20),
             SUBSTR(T.��סַ�ʱ�, 0, 6),
             T.���ڵ�ַ,
             SUBSTR(T.������������, 0, 6),
             T.������λ��ַ,
             SUBSTR(T.�����绰, 0, 20),
             SUBSTR(T.������������, 0, 6),
             SUBSTR(T.��ϵ������, 0, 20),
             T.��ϵ,
             T.��ϵ�˵�ַ,
             SUBSTR(T.��ϵ�˵绰, 0, 20),
             T.��Ժ;��,
             T.�������,
             TO_CHAR(T.��Ժ����, 'yyyy-MM-dd') AS ��Ժ����,
             TO_CHAR(T.��Ժ����, 'HH') ʱ,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             (SELECT LISTAGG(ת�����ұ���, '��') WITHIN GROUP(ORDER BY ת��ʱ��) AS ת�ƿƱ�
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM <= 3) ת�ƿƱ�,
             TO_CHAR(T.��Ժ����, 'yyyy-MM-dd') AS ��Ժ����,
             TO_CHAR(T.��Ժ����, 'HH') ʱ,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             T.סԺ����,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 2) AS �����������,
             SUBSTR((SELECT (CASE
                             WHEN A.ICD�� = A.�������� THEN
                              (SELECT B.ICD��
                                 FROM ������Ŀ_�����ֵ� B
                                WHERE B.�������� = A.��������)
                             ELSE
                              A.ICD��
                           END) ICD��
                      FROM סԺ����_��Ժ������� A
                     WHERE A.�������� = STR_��������
                       AND A.סԺ������ = T.סԺ������
                       AND A.������� = '�������'
                       AND A.��Ϸ��� = '1'
                       AND ROWNUM = 2),
                    0,
                    16) AS ��������,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �����������,
             SUBSTR((SELECT (CASE
                             WHEN A.ICD�� = A.�������� THEN
                              (SELECT B.ICD��
                                 FROM ������Ŀ_�����ֵ� B
                                WHERE B.�������� = A.��������)
                             ELSE
                              A.ICD��
                           END) ICD��
                      FROM סԺ����_��Ժ������� A
                     WHERE A.�������� = STR_��������
                       AND A.סԺ������ = T.סԺ������
                       AND A.������� = '�������'
                       AND A.��Ϸ��� = '1'
                       AND ROWNUM = 1),
                    0,
                    16) AS ��������,
             T.ʵʩ�ٴ�·��,
             T.ʹ��ҽ�ƻ�����ҩ�Ƽ�,
             T.ʹ����ҽ�����豸,
             T.ʹ����ҽ���Ƽ���,
             T.��֤ʩ��,
             --ʡ���������
             T.�����,
             DECODE(T.ҩ�����, '1', '��', '2', '��'),
             T.����ҩ��,
             DECODE(T.ʬ��, '��', '0', '��', '1', '0') ʬ��,
             T.Ѫ��,
             T.RH,
             T.���ȴ���,
             T.�ɹ�����,
             T.��Ѫ��Ӧ,
             T.÷��,
             '' ����������ɸ��,
             '' �����Ѫ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.������) AS ������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.סԺҽʦ) AS סԺҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.���λ�ʿ) AS ���λ�ʿ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.ʵϰҽʦ) AS ʵϰҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����Ա) AS ����Ա,
             T.��������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿ�ҽ��) AS �ʿ�ҽ��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿػ�ʿ) AS �ʿػ�ʿ,
             TO_CHAR(TO_DATE(T.�ʿ�����, 'yyyy-MM-dd hh24:mi:ss'),
                     'yyyy-MM-dd') �ʿ�����,
             --ʡ����������
             T.��Ժ��ʽ,
             T.�Ƿ��г�Ժ31����סԺ�ƻ�,
             T.Ŀ��,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ��,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ��,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����ԺǰСʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����ԺǰСʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ��Сʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ��Сʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����
        FROM סԺ����_������ҳ T, סԺ����_��Ժ������Ϣ TT
       WHERE T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         and T.������� != '3'
         AND TT.�������ͱ��� = '2'
         AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
         AND T.�鵵�˱��� IS NOT NULL
         AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
             T.������ LIKE '%' || STR_�������� || '%' OR
             T.�������� LIKE '%' || STR_�������� || '%');
  
  END;

  --���·��ü�¼��Ϣ
  FOR ROW_���ü�¼ IN CUR_���ü�¼ LOOP
    EXIT WHEN CUR_���ü�¼%NOTFOUND;
    UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ T
       SET T.ZFY      = ROW_���ü�¼.�ܽ��,
           T.ZFJE     = ROW_���ü�¼.�Ը����,
           T.ZFIJE    = ROW_���ü�¼.�Ը����,
           T.QTZF    =
           (ROW_���ü�¼.�ܽ�� - ROW_���ü�¼.�Ը����),
           T.YLFWF    = ROW_���ü�¼.һ��ҽ�Ʒ����,
           T.BZLZF    = ROW_���ü�¼.����_��ҽ��֤���η�,
           T.ZYBLZHZF = ROW_���ü�¼.����_��ҽ��֤���λ����,
           T.ZLCZF    = ROW_���ü�¼.һ�����Ʋ�����,
           T.HLF      = ROW_���ü�¼.�����,
           T.QTFY     = ROW_���ü�¼.��������,
           T.BLZDF    = ROW_���ü�¼.������Ϸ�,
           T.SYSZDF   = ROW_���ü�¼.ʵ������Ϸ�,
           T.YXXZDF   = ROW_���ü�¼.Ӱ��ѧ��Ϸ�,
           T.LCZDXMF  = ROW_���ü�¼.�ٴ������Ŀ��,
           T.FSSZLXMF = ROW_���ü�¼.������������Ŀ��,
           T.ZLF      = ROW_���ü�¼.����_�ٴ��������Ʒ�,
           T.SSZLF    = ROW_���ü�¼.�������Ʒ�,
           T.MZF      = ROW_���ü�¼.����_�����,
           T.SSF      = ROW_���ü�¼.����_������,
           T.KFF      = ROW_���ü�¼.������,
           T.ZYL_ZYZD = ROW_���ü�¼.��ҽ���,
           T.ZYZL     = ROW_���ü�¼.��ҽ����,
           T.ZYWZ     = ROW_���ü�¼.����_��ҽ����,
           T.ZYGS     = ROW_���ü�¼.����_��ҽ����,
           T.ZCYJF    = ROW_���ü�¼.����_�����ķ�,
           T.ZYTNZL   = ROW_���ü�¼.����_��ҽ��������,
           T.ZYGCZL   = ROW_���ü�¼.����_��ҽ�س�����,
           T.ZYTSZL   = ROW_���ü�¼.����_��ҽ��������,
           T.ZYQT     = ROW_���ü�¼.��ҽ����,
           T.ZYTSTPJG = ROW_���ü�¼.����_��ҽ�������ӹ�,
           T.BZSS     = ROW_���ü�¼.����_��֤ʩ��,
           T.XYF      = ROW_���ü�¼.��ҩ��,
           T.KJYWF    = ROW_���ü�¼.����_����ҩ���,
           T.ZCYF     = ROW_���ü�¼.�г�ҩ��,
           T.YZJF_ZCY = ROW_���ü�¼.����_ҽ�ƻ�����ҩ�Ƽ���,
           T.ZCYF1    = ROW_���ü�¼.�в�ҩ��,
           T.XF       = ROW_���ü�¼.Ѫ��,
           T.BDBLZPF  = ROW_���ü�¼.�׵�������Ʒ��,
           T.QDBLZPF  = ROW_���ü�¼.�򵰰�����Ʒ��,
           T.NXYZLZPF = ROW_���ü�¼.��Ѫ��������Ʒ��,
           T.XBYZLZPF = ROW_���ü�¼.ϸ����������Ʒ��,
           T.CYYYCLF  = ROW_���ü�¼.�����һ����ҽ�ò��Ϸ�,
           T.YYCLF    = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.SSYCXCLF = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.QTF      = ROW_���ü�¼.������
    
     WHERE T.JYH = ROW_���ü�¼.סԺ������;
  END LOOP;

  --����������¼��Ϣ
  FOR ROW_������¼ IN CUR_������¼ LOOP
    EXIT WHEN CUR_������¼%NOTFOUND;
  
    STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET SSBM' || ROW_������¼.RN ||
               '_S=:1, SSJCZRQ' || ROW_������¼.RN || '=:2, SSJB' ||
               ROW_������¼.RN || '= :3
               , SSJCZMC' || ROW_������¼.RN || '=:4, SZ' ||
               ROW_������¼.RN || '=:5, YZ' || ROW_������¼.RN || '=:6, EZ' ||
               ROW_������¼.RN || '=:7, QKYHDJ' || ROW_������¼.RN || '=:8, QKYHLB' ||
               ROW_������¼.RN || '=:9, MZFS' || ROW_������¼.RN || '=:10, MZYS' ||
               ROW_������¼.RN || '=:11 WHERE JYH=:12';
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_������¼.������, ROW_������¼.������������, ROW_������¼.�����������, ROW_������¼.������������, ROW_������¼.����, ROW_������¼.I��, ROW_������¼.II��, ROW_������¼.�пڵȼ�, ROW_������¼.�п����ϵȼ�, ROW_������¼.����ʽ����, ROW_������¼.����ҽʦ, ROW_������¼.סԺ������;
  
  END LOOP;

  --������ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼ IN CUR_��ϼ�¼ LOOP
    EXIT WHEN CUR_��ϼ�¼%NOTFOUND;
    IF ROW_��ϼ�¼.������� = '��Ժ���' THEN
      IF ROW_��ϼ�¼.��Ϸ��� = '2' then
        --��ҽ
        STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET ZB=:1,ZBBM_S=:2,ZB_RYBQ=:3,
                   ZZ1=:4,ZZBM1_S=:5,ZZ_RYBQ1=:6 ,
                   ZZ2=:7,ZZBM2_S=:8,ZZ_RYBQ2=:9,
                   ZZ3=:10,ZZBM3_S=:11,ZZ_RYBQ3=:12,
                   ZZ4=:13,ZZBM4_S=:14,ZZ_RYBQ4=:15, 
                   ZZ5=:16,ZZBM5_S=:17,ZZ_RYBQ5=:18, 
                   ZZ6=:19,ZZBM6_S=:20,ZZ_RYBQ6=:21, 
                   ZZ7=:22,ZZBM7_S=:23,ZZ_RYBQ7=:24, 
                   ZZ8=:25,ZZBM8_S=:26,ZZ_RYBQ8=:27, 
                   ZZ9=:28,ZZBM9_S=:29,ZZ_RYBQ9=:30
                   WHERE JYH=:31';
      
        EXECUTE IMMEDIATE STR_SQL
          using ROW_��ϼ�¼.��������, SUBSTR(ROW_��ϼ�¼.ICD��, 0, 6), ROW_��ϼ�¼.��Ժ����, 
          ROW_��ϼ�¼.��������1, SUBSTR(ROW_��ϼ�¼.ICD��1, 0, 6), ROW_��ϼ�¼.��Ժ����1, 
          ROW_��ϼ�¼.��������2, SUBSTR(ROW_��ϼ�¼.ICD��2, 0, 6), ROW_��ϼ�¼.��Ժ����2, 
          ROW_��ϼ�¼.��������3, SUBSTR(ROW_��ϼ�¼.ICD��3, 0, 6), ROW_��ϼ�¼.��Ժ����3, 
          ROW_��ϼ�¼.��������4, SUBSTR(ROW_��ϼ�¼.ICD��4, 0, 6), ROW_��ϼ�¼.��Ժ����4, 
          ROW_��ϼ�¼.��������5, SUBSTR(ROW_��ϼ�¼.ICD��5, 0, 6), ROW_��ϼ�¼.��Ժ����5, 
          ROW_��ϼ�¼.��������6, SUBSTR(ROW_��ϼ�¼.ICD��6, 0, 6), ROW_��ϼ�¼.��Ժ����6, 
          ROW_��ϼ�¼.��������7, SUBSTR(ROW_��ϼ�¼.ICD��7, 0, 6), ROW_��ϼ�¼.��Ժ����7, 
          ROW_��ϼ�¼.��������8, SUBSTR(ROW_��ϼ�¼.ICD��8, 0, 6), ROW_��ϼ�¼.��Ժ����8, 
          ROW_��ϼ�¼.��������9, SUBSTR(ROW_��ϼ�¼.ICD��9, 0, 6), ROW_��ϼ�¼.��Ժ����9, 
          ROW_��ϼ�¼.סԺ������;
      else
        --��ҽ
        STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET ZYZD=:1,ZYZDBM_S=:2,XY_RYBQ=:3 WHERE JYH=:4';
        EXECUTE IMMEDIATE STR_SQL
          USING ROW_��ϼ�¼.��������, SUBSTR(ROW_��ϼ�¼.ICD��, 0, 10), ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
      end if;
    
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET QTZD' || ROW_��ϼ�¼.RN ||
                 '=:1,QTZDBM' || ROW_��ϼ�¼.RN || '_S=:2,RYBQ' || ROW_��ϼ�¼.RN ||
                 '=:3 WHERE JYH=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, SUBSTR(ROW_��ϼ�¼.ICD��, 0, 10), ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '���˺��ж��ⲿԭ��' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET WBYY=:1,JBBM1_S=:2 WHERE JYH=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, SUBSTR(ROW_��ϼ�¼.ICD��, 0, 10), ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ��ҽ_ҽ������ SET BLZD=:1,JBBM2_S=:2 WHERE JYH=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, SUBSTR(ROW_��ϼ�¼.ICD��, 0, 10), ROW_��ϼ�¼.סԺ������;
    END IF;
  END LOOP;

  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT T.* FROM ��ʱ��_������ҳ��ҽ_ҽ������ T WHERE T.ZYZD!='�޷���Ժ';
END PR_������ҳ��ҽ_ҽ������;
/
