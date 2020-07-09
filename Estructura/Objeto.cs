﻿using System.Collections;
using Grafica.MyGame;
using OpenTK;

namespace Grafica.Estructura
{
    abstract class Objeto
    {
        public Vector3 center = Vector3.Zero;

        public Vector3 translation = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public Vector3 scale = Vector3.One;

        public Matrix4 modelObject = Matrix4.Identity;

        public Movement movement;

        public int partCount = 0;
        public Hashtable parts = new Hashtable();

        public string key;
        public bool obj;

        public abstract void CalculateMatrix();
    }
}
