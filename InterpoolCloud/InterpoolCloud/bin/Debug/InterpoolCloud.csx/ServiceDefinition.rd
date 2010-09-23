<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="InterpoolCloud" generation="1" functional="0" release="0" Id="250aa8ef-09f4-46ee-ba9d-c76ee1f467f8" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="InterpoolCloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="InterpoolCloudWebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/InterpoolCloud/InterpoolCloudGroup/LB:InterpoolCloudWebRole:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="InterpoolCloudWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/InterpoolCloud/InterpoolCloudGroup/MapInterpoolCloudWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="InterpoolCloudWebRole:DiagnosticsConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/InterpoolCloud/InterpoolCloudGroup/MapInterpoolCloudWebRole:DiagnosticsConnectionString" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:InterpoolCloudWebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/InterpoolCloud/InterpoolCloudGroup/InterpoolCloudWebRole/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapInterpoolCloudWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/InterpoolCloud/InterpoolCloudGroup/InterpoolCloudWebRoleInstances" />
          </setting>
        </map>
        <map name="MapInterpoolCloudWebRole:DiagnosticsConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/InterpoolCloud/InterpoolCloudGroup/InterpoolCloudWebRole/DiagnosticsConnectionString" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="InterpoolCloudWebRole" generation="1" functional="0" release="0" software="C:\Users\Vicente\Documents\Facultad\4to\Proyecto Ingeniería de Software\Repositorio\trunk\InterpoolCloud\InterpoolCloud\bin\Debug\InterpoolCloud.csx\roles\InterpoolCloudWebRole" entryPoint="base\x64\WaWebHost.exe" parameters="" memIndex="1792" hostingEnvironment="frontendfulltrust" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" />
            </componentports>
            <settings>
              <aCS name="DiagnosticsConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;InterpoolCloudWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;InterpoolCloudWebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/InterpoolCloud/InterpoolCloudGroup/InterpoolCloudWebRoleInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="InterpoolCloudWebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="82ea30e5-efbf-46f0-989c-a7cbaa415a72" ref="Microsoft.RedDog.Contract\ServiceContract\InterpoolCloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="211076dc-060d-46be-b982-92c9eba6e6c9" ref="Microsoft.RedDog.Contract\Interface\InterpoolCloudWebRole:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/InterpoolCloud/InterpoolCloudGroup/InterpoolCloudWebRole:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>