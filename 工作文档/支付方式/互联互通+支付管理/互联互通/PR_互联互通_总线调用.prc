CREATE OR REPLACE PROCEDURE PR_������ͨ_���ߵ���(STR_���ܺ�   IN VARCHAR2,
                                         STR_������� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         RES_CODE     OUT VARCHAR2,
                                         RES_MSG      OUT VARCHAR2) IS
  STR_ƽ̨��ʶ     VARCHAR2(10) := '12320';
  STR_״̬         VARCHAR2(50);
  STR_�Ƿ������Ű� VARCHAR2(50);
BEGIN

  BEGIN
    SELECT ״̬
      INTO STR_״̬
      FROM ������ͨ_ƽ̨��������
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ���ܱ��� = STR_���ܺ�;
  EXCEPTION
    WHEN OTHERS THEN
      STR_״̬ := '0';
  END;
  IF STR_״̬ = '0' THEN
    RES_CODE := '-1';
    RES_MSG  := '�ù�����δ����';
    RETURN;
  END IF;

  --�����Ƿ������Ű����
  BEGIN
    SELECT ֵ
      INTO STR_�Ƿ������Ű�
      FROM ������Ŀ_���������б�
     WHERE �������룽 '910536'
       AND �������� = '522633020000001';
  EXCEPTION
    WHEN OTHERS THEN
      STR_�Ƿ������Ű� := '��';
  END;
  
   IF STR_�Ƿ������Ű� = '��' THEN
    RES_CODE := '-1';
    RES_MSG  := '�ù�����δ����';
    RETURN;
  END IF;

  BEGIN
    IF STR_���ܺ� = '1001' THEN
      LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����('SELECT TO_CHAR(SYSDATE,''yyyy-MM-dd hh24:mi:ss'') AS "SYSDATE" FROM DUAL',
                                 'RES',
                                 '');
      RES_CODE     := '0';
      RES_MSG      := '���׳ɹ�';
    ELSIF STR_���ܺ� = '1002' THEN
      PR_������ͨ_�û���Ϣע��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1003' THEN
      PR_������ͨ_�û���Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1004' THEN
      PR_������ͨ_ҽԺ��Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1005' THEN
      RES_CODE := 200502;
      RES_MSG  := '�û�����Ϣ��ƥ��';
    ELSIF STR_���ܺ� = '2001' THEN
      PR_������ͨ_���Ҳ�ѯ(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2002' THEN
      PR_������ͨ_ҽ����ѯ(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2003' THEN
      PR_������ͨ_�Ű���Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2004' THEN
      PR_������ͨ_�Ű��ʱ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2005' THEN
      PR_������ͨ_��Դ����(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2006' THEN
      PR_������ͨ_��Դ����(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2007' THEN
      PR_������ͨ_ԤԼ�ҺŵǼ�(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2008' THEN
      PR_������ͨ_ԤԼ�Һ�֧��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2009' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2010' THEN
      PR_������ͨ_ԤԼ�Һ��˿�(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2011' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     STR_���ñ�ʶ => '0',
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2012' THEN
      PR_������ͨ_ԤԼ�Һż�¼��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2020' THEN
      PR_������ͨ_ҽ���������ݲ�ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3001' THEN
      PR_������ͨ_�ɷѼ�¼��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3002' THEN
      PR_������ͨ_�ɷ���ϸ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3003' THEN
      PR_������ͨ_�ɷѵ�֧��(STR_������� => STR_�������,
                    STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                    STR_���ܱ��� => STR_���ܺ�,
                    LOB_��Ӧ���� => LOB_��Ӧ����,
                    INT_����ֵ   => RES_CODE,
                    STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3004' THEN
      PR_������ͨ_�ɷѶ�����ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '4001' THEN
      RES_CODE := 400101;
      RES_MSG  := '�ŶӼ�¼�����ڣ�δ��ѯ���ŶӼ�¼';
    ELSIF STR_���ܺ� = '8001' THEN
      PR_������ͨ_�������б��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8002' THEN
      PR_������ͨ_���鱨���ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8003' THEN
      PR_������ͨ_���鱨���ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8004' THEN
      PR_������ͨ_��鱨���ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '9003' THEN
      PR_������ͨ_ϵͳ������ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    
    ELSIF STR_���ܺ� = '5004' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     STR_���ñ�ʶ => '1',
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSE
      RES_CODE := '-1';
      RES_MSG  := '���ܺŴ���';
    END IF;
    RETURN;
  END;

END PR_������ͨ_���ߵ���;
/
