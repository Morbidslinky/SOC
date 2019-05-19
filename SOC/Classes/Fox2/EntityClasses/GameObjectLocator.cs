﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Fox2
{
    class GameObjectLocator : Fox2EntityClass
    {
        private string name, typeName;
        private Fox2EntityClass dataSet, transform, parameters;

        public GameObjectLocator(string _name, Fox2EntityClass _dataSet, Fox2EntityClass _transform, string _typeName, Fox2EntityClass _parameters)
        {
            name = _name; typeName = _typeName;
            dataSet = _dataSet; transform = _transform; parameters = _parameters;
        }

        public override string GetFox2Format()
        {
            return string.Format($@"
                                  <entity class=""GameObjectLocator"" classVersion=""2"" addr=""{GetHexAddress()}"" unknown1=""272"" unknown2=""29247"">
                                    <staticProperties>
                                      <property name=""name"" type=""String"" container=""StaticArray"" arraySize=""1"">
                                         <value>{name}</value>
                                      </property>
                                      <property name=""dataSet"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
                                          <value>{dataSet.GetHexAddress()}</value>
                                      </property>
                                      <property name=""parent"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
                                        <value>0x00000000</value>
                                      </property>
                                      <property name=""transform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
                                          <value>{transform.GetHexAddress()}</value>
                                      </property>
                                      <property name=""shearTransform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
                                        <value>0x00000000</value>
                                      </property>
                                      <property name=""pivotTransform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
                                        <value>0x00000000</value>
                                      </property>
                                      <property name=""children"" type=""EntityHandle"" container=""List"" />
                                      <property name=""flags"" type=""uint32"" container=""StaticArray"" arraySize=""1"">
                                        <value>7</value>
                                      </property>
                                      <property name=""typeName"" type=""String"" container=""StaticArray"" arraySize=""1"">
                                          <value>{typeName}</value>
                                      </property>
                                      <property name=""groupId"" type=""uint32"" container=""StaticArray"" arraySize=""1"">
                                        <value>0</value>
                                      </property>
                                      <property name=""parameters"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
                                          <value>{parameters.GetHexAddress()}</value>
                                      </property>
                                    </staticProperties>
                                    <dynamicProperties />
                                  </entity>
                                ");
        }

        public override Fox2EntityClass GetOwner()
        {
            return dataSet;
        }

        public override string GetName()
        {
            return name;
        }
    }
}