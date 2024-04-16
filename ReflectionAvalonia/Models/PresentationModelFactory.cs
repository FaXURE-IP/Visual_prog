using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionAvalonia.Models
{
    public class PresentationModelFactory
    {
        private readonly HashSet<Type> baseTypes =
        [   typeof(string),
            typeof(int),
            typeof(double), 
            typeof(long),
            typeof(float),
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(char),
            typeof(decimal)
        ];
        private PresentationComponent BuildRecursivly(Type nodeType, PresentationComponent currNode,object property)
        {
            foreach (var prop in nodeType.GetProperties(

                BindingFlags.IgnoreCase 
                | BindingFlags.Instance 
                | BindingFlags.Public))
            {
                if (baseTypes.Contains(prop.PropertyType))
                {
                    if(property != null)
                    {
                        currNode.Add(
                            new PresentationLeaf(prop.Name,
                            prop.GetValue(property)));
                    }
                    else
                    {
                        currNode.Add(
                            new PresentationLeaf(prop.Name, 
                            "not initialized"));
                    }
                }
                else
                {
                    currNode.Add(BuildRecursivly(
                        prop.PropertyType, 
                        new PresentationComposite(prop.Name, ""),
                        prop.GetValue(property)));
                }
            }

            return currNode;
        }

        public PresentationComponent Create(object obj)
        {
            var type = obj.GetType();
            PresentationComponent component;

            if (type.GetProperties().Count() != 0)
            {
                component = new PresentationComposite(type.Name, obj.GetHashCode());
                BuildRecursivly(obj.GetType(), component,obj);

                return component;
            }
            else
            {
                return new PresentationLeaf(type.Name, obj);
            }
        }
    }
}
