<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="InterpoolPrototype" generation="1" functional="0" release="0" Id="149981b3-d5f2-43ad-87c5-64a92983a3db" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="InterpoolPrototypeGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="InterpoolPrototypeWebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/LB:InterpoolPrototypeWebRole:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="InterpoolPrototypeWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/MapInterpoolPrototypeWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="InterpoolPrototypeWebRole:DiagnosticsConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/MapInterpoolPrototypeWebRole:DiagnosticsConnectionString" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:InterpoolPrototypeWebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/InterpoolPrototypeWebRole/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapInterpoolPrototypeWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/InterpoolPrototypeWebRoleInstances" />
          </setting>
        </map>
        <map name="MapInterpoolPrototypeWebRole:DiagnosticsConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/InterpoolPrototypeWebRole/DiagnosticsConnectionString" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="InterpoolPrototypeWebRole" generation="1" functional="0" release="0" software="C:\Users\Martín\Documents\Visual Studio 2010\Projects\InterpoolPrototype\InterpoolPrototype\bin\Debug\InterpoolPrototype.csx\roles\InterpoolPrototypeWebRole" entryPoint="base\x64\WaWebHost.exe" parameters="" memIndex="1792" hostingEnvironment="frontendfulltrust" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" />
            </componentports>
            <settings>
              <aCS name="DiagnosticsConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;InterpoolPrototypeWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;InterpoolPrototypeWebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/InterpoolPrototypeWebRoleInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="InterpoolPrototypeWebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d1acb841-13ef-417e-93d4-335c26eaf0af" ref="Microsoft.RedDog.Contract\ServiceContract\InterpoolPrototypeContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="423964f5-1940-402f-a2bd-dda7c98515be" ref="Microsoft.RedDog.Contract\Interface\InterpoolPrototypeWebRole:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/InterpoolPrototype/InterpoolPrototypeGroup/InterpoolPrototypeWebRole:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>