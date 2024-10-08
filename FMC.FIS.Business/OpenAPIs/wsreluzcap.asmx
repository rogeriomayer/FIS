<?xml version="1.0" encoding="utf-8"?>
<wsdl:definitions xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://schemas.xmlsoap.org/wsdl/soap12/" xmlns:http="http://schemas.xmlsoap.org/wsdl/http/" xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/" xmlns:tns="https://www.twwwireless.com.br/reluzcap/wsreluzcap" xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:tm="http://microsoft.com/wsdl/mime/textMatching/" xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/" targetNamespace="https://www.twwwireless.com.br/reluzcap/wsreluzcap" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">
  <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Com esses serviços você pode enviar mensagens por CapCode para o sistema Reluz.</wsdl:documentation>
  <wsdl:types>
    <s:schema elementFormDefault="qualified" targetNamespace="https://www.twwwireless.com.br/reluzcap/wsreluzcap">
      <s:element name="EnviaSMS">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMS2SN">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum1" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum2" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Agendamento" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMS2SNResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMS2SNResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSQuebra">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSQuebraResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSQuebraResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoSemAcento">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Serie" type="s:int" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoSemAcentoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSConcatenadoSemAcentoResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoSemAcento2N">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum1" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum2" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Serie" type="s:int" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoSemAcento2NResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSConcatenadoSemAcento2NResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoComAcento">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Serie" type="s:int" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoComAcentoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSConcatenadoComAcentoResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoComAcento2N">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum1" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum2" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Serie" type="s:int" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSConcatenadoComAcento2NResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSConcatenadoComAcento2NResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAlt">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="user" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="pwd" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="msgid" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="phone" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="msgtext" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAltResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSAltResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAge">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Agendamento" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAgeResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSAgeResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSTemplate">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Template" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="VarNome" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Var1" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Var2" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Var3" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Var4" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Agendamento" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSTemplateResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSTemplateResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAgeQuebra">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Agendamento" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSAgeQuebraResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSAgeQuebraResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSDataSet">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="DS">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSDataSetResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSDataSetResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSXML">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="StrXML" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSXMLResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSXMLResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSTIM">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="XMLString" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSTIMResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSTIMResult">
              <s:complexType mixed="true">
                <s:sequence>
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSOTA8Bit">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Header" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Data" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSOTA8BitResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSOTA8BitResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSESMDCS">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="ESM" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="DCS" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Header" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Mensagem" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="EnviaSMSESMDCSResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="EnviaSMSESMDCSResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMS">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMSResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="StatusSMSResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMS2SN">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum1" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum2" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMS2SNResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="StatusSMS2SNResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMSDataSet">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="DS">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMSDataSetResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="StatusSMSDataSetResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMS">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="DataIni" type="s:dateTime" />
            <s:element minOccurs="1" maxOccurs="1" name="DataFim" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="BuscaSMSResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMO">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="DataIni" type="s:dateTime" />
            <s:element minOccurs="1" maxOccurs="1" name="DataFim" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMOResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="BuscaSMSMOResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSAgenda">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSAgendaResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="BuscaSMSAgendaResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSAgendaDataSet">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="DS">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSAgendaDataSetResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="BuscaSMSAgendaDataSetResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="DelSMSAgenda">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Agendamento" type="s:dateTime" />
            <s:element minOccurs="0" maxOccurs="1" name="SeuNum" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="DelSMSAgendaResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DelSMSAgendaResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="DelSMSAgendaIdLote">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="IdLote" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="DelSMSAgendaIdLoteResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="DelSMSAgendaIdLoteResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerBL">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerBLResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="VerBLResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="InsBL">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Celular" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="InsBLResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="InsBLResult" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerCredito">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerCreditoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="VerCreditoResult" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerValidade">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="VerValidadeResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="VerValidadeResult" type="s:dateTime" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="ResetaStatusLido">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="ResetaStatusLidoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ResetaStatusLidoResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="ResetaMOLido">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="ResetaMOLidoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ResetaMOLidoResult" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMSNaoLido">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="StatusSMSNaoLidoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="StatusSMSNaoLidoResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMONaoLido">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMONaoLidoResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="BuscaSMSMONaoLidoResult">
              <s:complexType>
                <s:sequence>
                  <s:element ref="s:schema" />
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMONaoLidoQuant">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="NumUsu" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Senha" type="s:string" />
            <s:element minOccurs="1" maxOccurs="1" name="Quant" type="s:int" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="BuscaSMSMONaoLidoQuantResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="1" maxOccurs="1" name="BuscaSMSMONaoLidoQuantResult" type="tns:MONLIDOQUANT" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:complexType name="MONLIDOQUANT">
        <s:sequence>
          <s:element minOccurs="1" maxOccurs="1" name="QUANTNL" type="s:int" />
          <s:element minOccurs="0" maxOccurs="1" name="DS">
            <s:complexType>
              <s:sequence>
                <s:element ref="s:schema" />
                <s:any />
              </s:sequence>
            </s:complexType>
          </s:element>
        </s:sequence>
      </s:complexType>
      <s:element name="string" nillable="true" type="s:string" />
      <s:element name="DataSet" nillable="true">
        <s:complexType>
          <s:sequence>
            <s:element ref="s:schema" />
            <s:any />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="int" type="s:int" />
      <s:element name="dateTime" type="s:dateTime" />
      <s:element name="MONLIDOQUANT" type="tns:MONLIDOQUANT" />
    </s:schema>
  </wsdl:types>
  <wsdl:message name="EnviaSMSSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMS" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMS2SN" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMS2SNResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSQuebra" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSQuebraResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoSemAcento" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoSemAcentoResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoSemAcento2N" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoSemAcento2NResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoComAcento" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoComAcentoResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoComAcento2N" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSConcatenadoComAcento2NResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSAlt" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSAltResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSAge" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSAgeResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSTemplate" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSTemplateResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSAgeQuebra" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSAgeQuebraResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSDataSetSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSDataSet" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSDataSetSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSDataSetResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSXML" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSXMLResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSTIM" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSTIMResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSOTA8Bit" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSOTA8BitResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSSoapIn">
    <wsdl:part name="parameters" element="tns:EnviaSMSESMDCS" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSSoapOut">
    <wsdl:part name="parameters" element="tns:EnviaSMSESMDCSResponse" />
  </wsdl:message>
  <wsdl:message name="StatusSMSSoapIn">
    <wsdl:part name="parameters" element="tns:StatusSMS" />
  </wsdl:message>
  <wsdl:message name="StatusSMSSoapOut">
    <wsdl:part name="parameters" element="tns:StatusSMSResponse" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNSoapIn">
    <wsdl:part name="parameters" element="tns:StatusSMS2SN" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNSoapOut">
    <wsdl:part name="parameters" element="tns:StatusSMS2SNResponse" />
  </wsdl:message>
  <wsdl:message name="StatusSMSDataSetSoapIn">
    <wsdl:part name="parameters" element="tns:StatusSMSDataSet" />
  </wsdl:message>
  <wsdl:message name="StatusSMSDataSetSoapOut">
    <wsdl:part name="parameters" element="tns:StatusSMSDataSetResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMS" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMSMO" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSMOResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMSAgenda" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSAgendaResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaDataSetSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMSAgendaDataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaDataSetSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSAgendaDataSetResponse" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaSoapIn">
    <wsdl:part name="parameters" element="tns:DelSMSAgenda" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaSoapOut">
    <wsdl:part name="parameters" element="tns:DelSMSAgendaResponse" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteSoapIn">
    <wsdl:part name="parameters" element="tns:DelSMSAgendaIdLote" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteSoapOut">
    <wsdl:part name="parameters" element="tns:DelSMSAgendaIdLoteResponse" />
  </wsdl:message>
  <wsdl:message name="VerBLSoapIn">
    <wsdl:part name="parameters" element="tns:VerBL" />
  </wsdl:message>
  <wsdl:message name="VerBLSoapOut">
    <wsdl:part name="parameters" element="tns:VerBLResponse" />
  </wsdl:message>
  <wsdl:message name="InsBLSoapIn">
    <wsdl:part name="parameters" element="tns:InsBL" />
  </wsdl:message>
  <wsdl:message name="InsBLSoapOut">
    <wsdl:part name="parameters" element="tns:InsBLResponse" />
  </wsdl:message>
  <wsdl:message name="VerCreditoSoapIn">
    <wsdl:part name="parameters" element="tns:VerCredito" />
  </wsdl:message>
  <wsdl:message name="VerCreditoSoapOut">
    <wsdl:part name="parameters" element="tns:VerCreditoResponse" />
  </wsdl:message>
  <wsdl:message name="VerValidadeSoapIn">
    <wsdl:part name="parameters" element="tns:VerValidade" />
  </wsdl:message>
  <wsdl:message name="VerValidadeSoapOut">
    <wsdl:part name="parameters" element="tns:VerValidadeResponse" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoSoapIn">
    <wsdl:part name="parameters" element="tns:ResetaStatusLido" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoSoapOut">
    <wsdl:part name="parameters" element="tns:ResetaStatusLidoResponse" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoSoapIn">
    <wsdl:part name="parameters" element="tns:ResetaMOLido" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoSoapOut">
    <wsdl:part name="parameters" element="tns:ResetaMOLidoResponse" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoSoapIn">
    <wsdl:part name="parameters" element="tns:StatusSMSNaoLido" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoSoapOut">
    <wsdl:part name="parameters" element="tns:StatusSMSNaoLidoResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMSMONaoLido" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSMONaoLidoResponse" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantSoapIn">
    <wsdl:part name="parameters" element="tns:BuscaSMSMONaoLidoQuant" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantSoapOut">
    <wsdl:part name="parameters" element="tns:BuscaSMSMONaoLidoQuantResponse" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltHttpGetIn">
    <wsdl:part name="user" type="s:string" />
    <wsdl:part name="pwd" type="s:string" />
    <wsdl:part name="msgid" type="s:string" />
    <wsdl:part name="phone" type="s:string" />
    <wsdl:part name="msgtext" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Template" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="VarNome" type="s:string" />
    <wsdl:part name="Var1" type="s:string" />
    <wsdl:part name="Var2" type="s:string" />
    <wsdl:part name="Var3" type="s:string" />
    <wsdl:part name="Var4" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="StrXML" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMHttpGetIn">
    <wsdl:part name="XMLString" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMHttpGetOut">
    <wsdl:part name="Body" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Header" type="s:string" />
    <wsdl:part name="Data" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="ESM" type="s:string" />
    <wsdl:part name="DCS" type="s:string" />
    <wsdl:part name="Header" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="DataIni" type="s:string" />
    <wsdl:part name="DataFim" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="DataIni" type="s:string" />
    <wsdl:part name="DataFim" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="IdLote" type="s:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="VerBLHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerBLHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="InsBLHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
  </wsdl:message>
  <wsdl:message name="InsBLHttpGetOut">
    <wsdl:part name="Body" element="tns:int" />
  </wsdl:message>
  <wsdl:message name="VerCreditoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerCreditoHttpGetOut">
    <wsdl:part name="Body" element="tns:int" />
  </wsdl:message>
  <wsdl:message name="VerValidadeHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerValidadeHttpGetOut">
    <wsdl:part name="Body" element="tns:dateTime" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoHttpGetOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoHttpGetOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantHttpGetIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Quant" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantHttpGetOut">
    <wsdl:part name="Body" element="tns:MONLIDOQUANT" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMS2SNHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSQuebraHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcentoHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoSemAcento2NHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcentoHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
    <wsdl:part name="Serie" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSConcatenadoComAcento2NHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltHttpPostIn">
    <wsdl:part name="user" type="s:string" />
    <wsdl:part name="pwd" type="s:string" />
    <wsdl:part name="msgid" type="s:string" />
    <wsdl:part name="phone" type="s:string" />
    <wsdl:part name="msgtext" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAltHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Template" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="VarNome" type="s:string" />
    <wsdl:part name="Var1" type="s:string" />
    <wsdl:part name="Var2" type="s:string" />
    <wsdl:part name="Var3" type="s:string" />
    <wsdl:part name="Var4" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTemplateHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSAgeQuebraHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="StrXML" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSXMLHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMHttpPostIn">
    <wsdl:part name="XMLString" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSTIMHttpPostOut">
    <wsdl:part name="Body" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="Header" type="s:string" />
    <wsdl:part name="Data" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSOTA8BitHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
    <wsdl:part name="ESM" type="s:string" />
    <wsdl:part name="DCS" type="s:string" />
    <wsdl:part name="Header" type="s:string" />
    <wsdl:part name="Mensagem" type="s:string" />
  </wsdl:message>
  <wsdl:message name="EnviaSMSESMDCSHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum1" type="s:string" />
    <wsdl:part name="SeuNum2" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMS2SNHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="DataIni" type="s:string" />
    <wsdl:part name="DataFim" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="DataIni" type="s:string" />
    <wsdl:part name="DataFim" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMOHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSAgendaHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Agendamento" type="s:string" />
    <wsdl:part name="SeuNum" type="s:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="IdLote" type="s:string" />
  </wsdl:message>
  <wsdl:message name="DelSMSAgendaIdLoteHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="VerBLHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerBLHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="InsBLHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Celular" type="s:string" />
  </wsdl:message>
  <wsdl:message name="InsBLHttpPostOut">
    <wsdl:part name="Body" element="tns:int" />
  </wsdl:message>
  <wsdl:message name="VerCreditoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerCreditoHttpPostOut">
    <wsdl:part name="Body" element="tns:int" />
  </wsdl:message>
  <wsdl:message name="VerValidadeHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="VerValidadeHttpPostOut">
    <wsdl:part name="Body" element="tns:dateTime" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="ResetaStatusLidoHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="ResetaMOLidoHttpPostOut">
    <wsdl:part name="Body" element="tns:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="StatusSMSNaoLidoHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoHttpPostOut">
    <wsdl:part name="Body" element="tns:DataSet" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantHttpPostIn">
    <wsdl:part name="NumUsu" type="s:string" />
    <wsdl:part name="Senha" type="s:string" />
    <wsdl:part name="Quant" type="s:string" />
  </wsdl:message>
  <wsdl:message name="BuscaSMSMONaoLidoQuantHttpPostOut">
    <wsdl:part name="Body" element="tns:MONLIDOQUANT" />
  </wsdl:message>
  <wsdl:portType name="ReluzCap_x0020_Web_x0020_ServiceSoap">
    <wsdl:operation name="EnviaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. A quantidade de caracteres suportados por SMS está sujeita a variação por operadora. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSSoapIn" />
      <wsdl:output message="tns:EnviaSMSSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular, usando 2 campos de referência ALFANUMÉRICOS (SeuNum1 e SeuNum2) de no máximo 20 caracteres cada. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMS2SNSoapIn" />
      <wsdl:output message="tns:EnviaSMS2SNSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto para um celular. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSQuebraSoapIn" />
      <wsdl:output message="tns:EnviaSMSQuebraSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado sem acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcentoSoapIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcentoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcento2NSoapIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcento2NSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado com acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 70 caracteres, ela será dividida em várias mensagens de até 70 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 2048 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcentoSoapIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcentoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcento2NSoapIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcento2NSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular utilizando url alternativa. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAltSoapIn" />
      <wsdl:output message="tns:EnviaSMSAltSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeSoapIn" />
      <wsdl:output message="tns:EnviaSMSAgeSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular obedecendo um template pré-cadastrado no WebCorp. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTemplateSoapIn" />
      <wsdl:output message="tns:EnviaSMSTemplateSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeQuebraSoapIn" />
      <wsdl:output message="tns:EnviaSMSAgeQuebraSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSDataSet">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um DataSet com mensagens SMS a serem enviadas, com os seguintes campos: seunum (Seu número com até 20 caracteres), idlote (opcional) (ID do lote com até 20 caracteres), celular (55DDNNNNNNNN), mensagem, agendamento (opcional) (dd/mm/aaaa hh:mm:ss). Retorna OK n (n é o número de SMSs recebidos), NOK, Erro ou NA (não disponível)</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSDataSetSoapIn" />
      <wsdl:output message="tns:EnviaSMSDataSetSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um XML com mensagens SMS a serem enviadas, com os seguintes campos: seunum (Seu número com até 20 caracteres), celular (55DDNNNNNNNN), mensagem, agendamento (dd/mm/aaaa hh:mm:ss). Retorna OK n (n é o número de SMSs recebidos), NOK, Erro ou NA (não disponível)</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSXMLSoapIn" />
      <wsdl:output message="tns:EnviaSMSXMLSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe uma String com um XML no mesmo formato usado para enviar SMS a operadora TIMSUL, para facilitar a integração com sistemas já desenvolvidos.</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTIMSoapIn" />
      <wsdl:output message="tns:EnviaSMSTIMSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem binária para um celular. Tanto o campo Header como o Data devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSOTA8BitSoapIn" />
      <wsdl:output message="tns:EnviaSMSOTA8BitSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. O campo ESM, DCS e Header devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. O campo Mensagem deve estar formatado como uma string de texto. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSESMDCSSoapIn" />
      <wsdl:output message="tns:EnviaSMSESMDCSSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSSoapIn" />
      <wsdl:output message="tns:StatusSMSSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMS2SNSoapIn" />
      <wsdl:output message="tns:StatusSMS2SNSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMSDataSet">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um DataSet com os campos: SeuNum, e retorna um DataSet chamado OutDataSet contendo a tabela StatusSMSDS com várias mensagens já transmitidas. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSDataSetSoapIn" />
      <wsdl:output message="tns:StatusSMSDataSetSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMS com as mensagens transmitidas dentro de um período MÁXIMO DE 4 DIAS, e um MÁXIMO DE 4000 SMSs. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSSoapIn" />
      <wsdl:output message="tns:BuscaSMSSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSMO com todas as mensagens SMS MO recebidas DENTRO DE UM PERIODO como resposta a SMS enviados anteriormente. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMOSoapIn" />
      <wsdl:output message="tns:BuscaSMSMOSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSAgenda com UM SMS AGENDADO ESPECIFICADO PELO CAMPO SEUNUM. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSAgendaSoapIn" />
      <wsdl:output message="tns:BuscaSMSAgendaSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgendaDataSet">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um DataSet com os campos: SeuNum, e retorna um DataSet chamado OutDataSet contendo a tabela BuscaSMSAgendaDS com mensagens AGENDADAS. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSAgendaDataSetSoapIn" />
      <wsdl:output message="tns:BuscaSMSAgendaDataSetSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta uma mensagem agendada. Retorna OK ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaSoapIn" />
      <wsdl:output message="tns:DelSMSAgendaSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta TODAS as mensagens agendadas que tenham o campo IdLote igual ao valor enviado como parâmetro. Retorna OK n, onde n é a quantidade de mensagens agendadas deletados, ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaIdLoteSoapIn" />
      <wsdl:output message="tns:DelSMSAgendaIdLoteSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet com os celulares incluidos na lista de bloqueio. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:VerBLSoapIn" />
      <wsdl:output message="tns:VerBLSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Insere um número de celular na lista de bloqueio. Retorna 1 em caso de sucesso, 0 caso o celular já esteja na lista de bloqueio, -1 em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:InsBLSoapIn" />
      <wsdl:output message="tns:InsBLSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Verifica os créditos de um Usuário Pré-Pago. Retorna o número de créditos ou -1 se o Usuário não for do tipo Pré-Pago ou -2 em caso de erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerCreditoSoapIn" />
      <wsdl:output message="tns:VerCreditoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna a data de validade dos créditos de um Usuário Pré-Pago. Retorna Nothing se o Usuário não for do tipo Pré-Pago ou caso haja erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerValidadeSoapIn" />
      <wsdl:output message="tns:VerValidadeSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos Status de SMS desde 1 dia atrás até a data atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaStatusLidoSoapIn" />
      <wsdl:output message="tns:ResetaStatusLidoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos SMS MO desde 1 dia atrás até o momento atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaMOLidoSoapIn" />
      <wsdl:output message="tns:ResetaMOLidoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com no máximo 400 linhas, contendo somente os status de SMS dos últimos 4 dias que ainda não tenham sido lidos, e os MARCA COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais status não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSNaoLidoSoapIn" />
      <wsdl:output message="tns:StatusSMSNaoLidoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada SMSMO com no máximo 400 linhas, com as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais MOs não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoSoapIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoSoapOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna uma estrutura MONLIDOQUANT, contendo um DataSet DS, chamado OutDataSet, com uma Tabela chamada SMSMO com o máximo de linhas definido pelo parâmetro de entrada Quant. O valor de Quant deve ser entre 400 e 2000, sendo ajustado caso especificado fora desse intervalo. A tabela contem as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. A estrutura contem, alem do DataSet um inteiro QUANTNL que indica quantos SMS MO NÃO LIDOS ainda existem no banco de dados, depois da presente execução. Estes MOs devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoQuantSoapIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoQuantSoapOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:portType name="ReluzCap_x0020_Web_x0020_ServiceHttpGet">
    <wsdl:operation name="EnviaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. A quantidade de caracteres suportados por SMS está sujeita a variação por operadora. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular, usando 2 campos de referência ALFANUMÉRICOS (SeuNum1 e SeuNum2) de no máximo 20 caracteres cada. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMS2SNHttpGetIn" />
      <wsdl:output message="tns:EnviaSMS2SNHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto para um celular. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSQuebraHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSQuebraHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado sem acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcentoHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcentoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcento2NHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcento2NHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado com acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 70 caracteres, ela será dividida em várias mensagens de até 70 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 2048 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcentoHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcentoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcento2NHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcento2NHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular utilizando url alternativa. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAltHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSAltHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSAgeHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular obedecendo um template pré-cadastrado no WebCorp. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTemplateHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSTemplateHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeQuebraHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSAgeQuebraHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um XML com mensagens SMS a serem enviadas, com os seguintes campos: seunum (Seu número com até 20 caracteres), celular (55DDNNNNNNNN), mensagem, agendamento (dd/mm/aaaa hh:mm:ss). Retorna OK n (n é o número de SMSs recebidos), NOK, Erro ou NA (não disponível)</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSXMLHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSXMLHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe uma String com um XML no mesmo formato usado para enviar SMS a operadora TIMSUL, para facilitar a integração com sistemas já desenvolvidos.</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTIMHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSTIMHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem binária para um celular. Tanto o campo Header como o Data devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSOTA8BitHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSOTA8BitHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. O campo ESM, DCS e Header devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. O campo Mensagem deve estar formatado como uma string de texto. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSESMDCSHttpGetIn" />
      <wsdl:output message="tns:EnviaSMSESMDCSHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSHttpGetIn" />
      <wsdl:output message="tns:StatusSMSHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMS2SNHttpGetIn" />
      <wsdl:output message="tns:StatusSMS2SNHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMS com as mensagens transmitidas dentro de um período MÁXIMO DE 4 DIAS, e um MÁXIMO DE 4000 SMSs. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSHttpGetIn" />
      <wsdl:output message="tns:BuscaSMSHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSMO com todas as mensagens SMS MO recebidas DENTRO DE UM PERIODO como resposta a SMS enviados anteriormente. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMOHttpGetIn" />
      <wsdl:output message="tns:BuscaSMSMOHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSAgenda com UM SMS AGENDADO ESPECIFICADO PELO CAMPO SEUNUM. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSAgendaHttpGetIn" />
      <wsdl:output message="tns:BuscaSMSAgendaHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta uma mensagem agendada. Retorna OK ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaHttpGetIn" />
      <wsdl:output message="tns:DelSMSAgendaHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta TODAS as mensagens agendadas que tenham o campo IdLote igual ao valor enviado como parâmetro. Retorna OK n, onde n é a quantidade de mensagens agendadas deletados, ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaIdLoteHttpGetIn" />
      <wsdl:output message="tns:DelSMSAgendaIdLoteHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet com os celulares incluidos na lista de bloqueio. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:VerBLHttpGetIn" />
      <wsdl:output message="tns:VerBLHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Insere um número de celular na lista de bloqueio. Retorna 1 em caso de sucesso, 0 caso o celular já esteja na lista de bloqueio, -1 em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:InsBLHttpGetIn" />
      <wsdl:output message="tns:InsBLHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Verifica os créditos de um Usuário Pré-Pago. Retorna o número de créditos ou -1 se o Usuário não for do tipo Pré-Pago ou -2 em caso de erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerCreditoHttpGetIn" />
      <wsdl:output message="tns:VerCreditoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna a data de validade dos créditos de um Usuário Pré-Pago. Retorna Nothing se o Usuário não for do tipo Pré-Pago ou caso haja erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerValidadeHttpGetIn" />
      <wsdl:output message="tns:VerValidadeHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos Status de SMS desde 1 dia atrás até a data atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaStatusLidoHttpGetIn" />
      <wsdl:output message="tns:ResetaStatusLidoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos SMS MO desde 1 dia atrás até o momento atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaMOLidoHttpGetIn" />
      <wsdl:output message="tns:ResetaMOLidoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com no máximo 400 linhas, contendo somente os status de SMS dos últimos 4 dias que ainda não tenham sido lidos, e os MARCA COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais status não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSNaoLidoHttpGetIn" />
      <wsdl:output message="tns:StatusSMSNaoLidoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada SMSMO com no máximo 400 linhas, com as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais MOs não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoHttpGetIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoHttpGetOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna uma estrutura MONLIDOQUANT, contendo um DataSet DS, chamado OutDataSet, com uma Tabela chamada SMSMO com o máximo de linhas definido pelo parâmetro de entrada Quant. O valor de Quant deve ser entre 400 e 2000, sendo ajustado caso especificado fora desse intervalo. A tabela contem as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. A estrutura contem, alem do DataSet um inteiro QUANTNL que indica quantos SMS MO NÃO LIDOS ainda existem no banco de dados, depois da presente execução. Estes MOs devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoQuantHttpGetIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoQuantHttpGetOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:portType name="ReluzCap_x0020_Web_x0020_ServiceHttpPost">
    <wsdl:operation name="EnviaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. A quantidade de caracteres suportados por SMS está sujeita a variação por operadora. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular, usando 2 campos de referência ALFANUMÉRICOS (SeuNum1 e SeuNum2) de no máximo 20 caracteres cada. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMS2SNHttpPostIn" />
      <wsdl:output message="tns:EnviaSMS2SNHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto para um celular. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSQuebraHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSQuebraHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado sem acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcentoHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcentoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 4096 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoSemAcento2NHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoSemAcento2NHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem de texto concatenado com acento para um celular. O campo Serie deve conter um número entre 0 e 255 e deve ser único para cada SMS concatenado enviado, sendo acrescido de 1 a cada envio, e quando atinge 255, comece com 0 (zero) novamente. Se essa mensagem for mais longa que 70 caracteres, ela será dividida em várias mensagens de até 70 caracteres e enviada de forma a chegar concatenada, em uma única mensagem, no celular de destino, desde que a operadora suporte concatenação. Se não houver suporte da operadora, a mensagem será enviada separadamente com + separando as mensagens. Tamanho máximo da mensagem = 2048 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcentoHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcentoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">O mesmo que EnviaSMSConcatenadoComAcento, com 2 campos de referência. Retorna OK n (n é o número de SMS enviados pela operação), NOK (usuário ou senha inválidos, ou mensagem maior que 2048 caracteres), Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSConcatenadoComAcento2NHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSConcatenadoComAcento2NHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular utilizando url alternativa. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAltHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSAltHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSAgeHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular obedecendo um template pré-cadastrado no WebCorp. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTemplateHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSTemplateHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular com agendamento. Se essa mensagem for mais longa que 140 caracteres, ela será dividida em várias mensagens de até 140 caracteres, com ... separando as mensagens. Tamanho máximo da mensagem = 4096 caracteres. Retorna OK n (n é o número de SMS enviados pela operação), NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSAgeQuebraHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSAgeQuebraHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe um XML com mensagens SMS a serem enviadas, com os seguintes campos: seunum (Seu número com até 20 caracteres), celular (55DDNNNNNNNN), mensagem, agendamento (dd/mm/aaaa hh:mm:ss). Retorna OK n (n é o número de SMSs recebidos), NOK, Erro ou NA (não disponível)</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSXMLHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSXMLHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Recebe uma String com um XML no mesmo formato usado para enviar SMS a operadora TIMSUL, para facilitar a integração com sistemas já desenvolvidos.</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSTIMHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSTIMHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem binária para um celular. Tanto o campo Header como o Data devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSOTA8BitHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSOTA8BitHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Envia uma mensagem para um celular. O campo ESM, DCS e Header devem estar no formato OTA 8 bit, com um número par de caracteres hexadecimais. O campo Mensagem deve estar formatado como uma string de texto. Retorna OK, NOK, Erro ou NA (não disponível).</wsdl:documentation>
      <wsdl:input message="tns:EnviaSMSESMDCSHttpPostIn" />
      <wsdl:output message="tns:EnviaSMSESMDCSHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSHttpPostIn" />
      <wsdl:output message="tns:StatusSMSHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com o status de UM SMS já transmitido. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMS2SNHttpPostIn" />
      <wsdl:output message="tns:StatusSMS2SNHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMS com as mensagens transmitidas dentro de um período MÁXIMO DE 4 DIAS, e um MÁXIMO DE 4000 SMSs. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSHttpPostIn" />
      <wsdl:output message="tns:BuscaSMSHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSMO com todas as mensagens SMS MO recebidas DENTRO DE UM PERIODO como resposta a SMS enviados anteriormente. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMOHttpPostIn" />
      <wsdl:output message="tns:BuscaSMSMOHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada BuscaSMSAgenda com UM SMS AGENDADO ESPECIFICADO PELO CAMPO SEUNUM. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSAgendaHttpPostIn" />
      <wsdl:output message="tns:BuscaSMSAgendaHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta uma mensagem agendada. Retorna OK ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaHttpPostIn" />
      <wsdl:output message="tns:DelSMSAgendaHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Deleta TODAS as mensagens agendadas que tenham o campo IdLote igual ao valor enviado como parâmetro. Retorna OK n, onde n é a quantidade de mensagens agendadas deletados, ou NOK.</wsdl:documentation>
      <wsdl:input message="tns:DelSMSAgendaIdLoteHttpPostIn" />
      <wsdl:output message="tns:DelSMSAgendaIdLoteHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet com os celulares incluidos na lista de bloqueio. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:VerBLHttpPostIn" />
      <wsdl:output message="tns:VerBLHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Insere um número de celular na lista de bloqueio. Retorna 1 em caso de sucesso, 0 caso o celular já esteja na lista de bloqueio, -1 em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:InsBLHttpPostIn" />
      <wsdl:output message="tns:InsBLHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Verifica os créditos de um Usuário Pré-Pago. Retorna o número de créditos ou -1 se o Usuário não for do tipo Pré-Pago ou -2 em caso de erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerCreditoHttpPostIn" />
      <wsdl:output message="tns:VerCreditoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna a data de validade dos créditos de um Usuário Pré-Pago. Retorna Nothing se o Usuário não for do tipo Pré-Pago ou caso haja erro nos parâmetros.</wsdl:documentation>
      <wsdl:input message="tns:VerValidadeHttpPostIn" />
      <wsdl:output message="tns:VerValidadeHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos Status de SMS desde 1 dia atrás até a data atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaStatusLidoHttpPostIn" />
      <wsdl:output message="tns:ResetaStatusLidoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Reseta o status de LIDO dos SMS MO desde 1 dia atrás até o momento atual. Retorna OK ou NOK em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:ResetaMOLidoHttpPostIn" />
      <wsdl:output message="tns:ResetaMOLidoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo a tabela StatusSMS com no máximo 400 linhas, contendo somente os status de SMS dos últimos 4 dias que ainda não tenham sido lidos, e os MARCA COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais status não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:StatusSMSNaoLidoHttpPostIn" />
      <wsdl:output message="tns:StatusSMSNaoLidoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna um DataSet chamado OutDataSet contendo uma Tabela chamada SMSMO com no máximo 400 linhas, com as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. Se houverem 400 linhas na tabela, podem haver mais MOs não lidos, e estes devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoHttpPostIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoHttpPostOut" />
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Retorna uma estrutura MONLIDOQUANT, contendo um DataSet DS, chamado OutDataSet, com uma Tabela chamada SMSMO com o máximo de linhas definido pelo parâmetro de entrada Quant. O valor de Quant deve ser entre 400 e 2000, sendo ajustado caso especificado fora desse intervalo. A tabela contem as mensagens SMS MO não lidas, recebidas nos últimos 4 dias como resposta a SMS enviados anteriormente, e marca esses MOs COMO LIDOS. A estrutura contem, alem do DataSet um inteiro QUANTNL que indica quantos SMS MO NÃO LIDOS ainda existem no banco de dados, depois da presente execução. Estes MOs devem ser lidos usando chamadas subsequentes à função. Retorna Nothing em caso de erro.</wsdl:documentation>
      <wsdl:input message="tns:BuscaSMSMONaoLidoQuantHttpPostIn" />
      <wsdl:output message="tns:BuscaSMSMONaoLidoQuantHttpPostOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:binding name="ReluzCap_x0020_Web_x0020_ServiceSoap" type="tns:ReluzCap_x0020_Web_x0020_ServiceSoap">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="EnviaSMS">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS2SN" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSQuebra" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento2N" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento2N" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAlt" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAge" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTemplate" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAgeQuebra" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSDataSet">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSDataSet" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSXML" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTIM" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSOTA8Bit" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSESMDCS" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS2SN" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSDataSet">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSDataSet" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMS" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMO" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgenda" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgendaDataSet">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgendaDataSet" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgenda" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgendaIdLote" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerBL" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/InsBL" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerCredito" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerValidade" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaStatusLido" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaMOLido" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSNaoLido" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLido" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <soap:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLidoQuant" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="ReluzCap_x0020_Web_x0020_ServiceSoap12" type="tns:ReluzCap_x0020_Web_x0020_ServiceSoap">
    <soap12:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="EnviaSMS">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS2SN" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSQuebra" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento2N" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento2N" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAlt" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAge" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTemplate" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAgeQuebra" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSDataSet">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSDataSet" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSXML" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTIM" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSOTA8Bit" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSESMDCS" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS2SN" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSDataSet">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSDataSet" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMS" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMO" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgenda" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgendaDataSet">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgendaDataSet" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgenda" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgendaIdLote" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerBL" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/InsBL" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerCredito" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerValidade" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaStatusLido" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaMOLido" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSNaoLido" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLido" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <soap12:operation soapAction="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLidoQuant" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="ReluzCap_x0020_Web_x0020_ServiceHttpGet" type="tns:ReluzCap_x0020_Web_x0020_ServiceHttpGet">
    <http:binding verb="GET" />
    <wsdl:operation name="EnviaSMS">
      <http:operation location="/EnviaSMS" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <http:operation location="/EnviaSMS2SN" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <http:operation location="/EnviaSMSQuebra" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <http:operation location="/EnviaSMSConcatenadoSemAcento" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <http:operation location="/EnviaSMSConcatenadoSemAcento2N" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <http:operation location="/EnviaSMSConcatenadoComAcento" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <http:operation location="/EnviaSMSConcatenadoComAcento2N" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <http:operation location="/EnviaSMSAlt" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <http:operation location="/EnviaSMSAge" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <http:operation location="/EnviaSMSTemplate" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <http:operation location="/EnviaSMSAgeQuebra" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <http:operation location="/EnviaSMSXML" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <http:operation location="/EnviaSMSTIM" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:content part="Body" type="text/xml" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <http:operation location="/EnviaSMSOTA8Bit" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <http:operation location="/EnviaSMSESMDCS" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <http:operation location="/StatusSMS" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <http:operation location="/StatusSMS2SN" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <http:operation location="/BuscaSMS" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <http:operation location="/BuscaSMSMO" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <http:operation location="/BuscaSMSAgenda" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <http:operation location="/DelSMSAgenda" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <http:operation location="/DelSMSAgendaIdLote" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <http:operation location="/VerBL" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <http:operation location="/InsBL" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <http:operation location="/VerCredito" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <http:operation location="/VerValidade" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <http:operation location="/ResetaStatusLido" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <http:operation location="/ResetaMOLido" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <http:operation location="/StatusSMSNaoLido" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <http:operation location="/BuscaSMSMONaoLido" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <http:operation location="/BuscaSMSMONaoLidoQuant" />
      <wsdl:input>
        <http:urlEncoded />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="ReluzCap_x0020_Web_x0020_ServiceHttpPost" type="tns:ReluzCap_x0020_Web_x0020_ServiceHttpPost">
    <http:binding verb="POST" />
    <wsdl:operation name="EnviaSMS">
      <http:operation location="/EnviaSMS" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMS2SN">
      <http:operation location="/EnviaSMS2SN" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSQuebra">
      <http:operation location="/EnviaSMSQuebra" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento">
      <http:operation location="/EnviaSMSConcatenadoSemAcento" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoSemAcento2N">
      <http:operation location="/EnviaSMSConcatenadoSemAcento2N" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento">
      <http:operation location="/EnviaSMSConcatenadoComAcento" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSConcatenadoComAcento2N">
      <http:operation location="/EnviaSMSConcatenadoComAcento2N" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAlt">
      <http:operation location="/EnviaSMSAlt" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAge">
      <http:operation location="/EnviaSMSAge" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTemplate">
      <http:operation location="/EnviaSMSTemplate" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSAgeQuebra">
      <http:operation location="/EnviaSMSAgeQuebra" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSXML">
      <http:operation location="/EnviaSMSXML" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSTIM">
      <http:operation location="/EnviaSMSTIM" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:content part="Body" type="text/xml" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSOTA8Bit">
      <http:operation location="/EnviaSMSOTA8Bit" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="EnviaSMSESMDCS">
      <http:operation location="/EnviaSMSESMDCS" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS">
      <http:operation location="/StatusSMS" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMS2SN">
      <http:operation location="/StatusSMS2SN" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMS">
      <http:operation location="/BuscaSMS" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMO">
      <http:operation location="/BuscaSMSMO" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSAgenda">
      <http:operation location="/BuscaSMSAgenda" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgenda">
      <http:operation location="/DelSMSAgenda" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="DelSMSAgendaIdLote">
      <http:operation location="/DelSMSAgendaIdLote" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerBL">
      <http:operation location="/VerBL" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="InsBL">
      <http:operation location="/InsBL" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerCredito">
      <http:operation location="/VerCredito" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="VerValidade">
      <http:operation location="/VerValidade" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaStatusLido">
      <http:operation location="/ResetaStatusLido" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="ResetaMOLido">
      <http:operation location="/ResetaMOLido" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="StatusSMSNaoLido">
      <http:operation location="/StatusSMSNaoLido" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLido">
      <http:operation location="/BuscaSMSMONaoLido" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
    <wsdl:operation name="BuscaSMSMONaoLidoQuant">
      <http:operation location="/BuscaSMSMONaoLidoQuant" />
      <wsdl:input>
        <mime:content type="application/x-www-form-urlencoded" />
      </wsdl:input>
      <wsdl:output>
        <mime:mimeXml part="Body" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:service name="ReluzCap_x0020_Web_x0020_Service">
    <wsdl:documentation xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">Com esses serviços você pode enviar mensagens por CapCode para o sistema Reluz.</wsdl:documentation>
    <wsdl:port name="ReluzCap_x0020_Web_x0020_ServiceSoap" binding="tns:ReluzCap_x0020_Web_x0020_ServiceSoap">
      <soap:address location="https://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx" />
    </wsdl:port>
    <wsdl:port name="ReluzCap_x0020_Web_x0020_ServiceSoap12" binding="tns:ReluzCap_x0020_Web_x0020_ServiceSoap12">
      <soap12:address location="https://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx" />
    </wsdl:port>
    <wsdl:port name="ReluzCap_x0020_Web_x0020_ServiceHttpGet" binding="tns:ReluzCap_x0020_Web_x0020_ServiceHttpGet">
      <http:address location="https://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx" />
    </wsdl:port>
    <wsdl:port name="ReluzCap_x0020_Web_x0020_ServiceHttpPost" binding="tns:ReluzCap_x0020_Web_x0020_ServiceHttpPost">
      <http:address location="https://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx" />
    </wsdl:port>
  </wsdl:service>
</wsdl:definitions>