using Grafica.Estructura;
using Grafica.MyGame.Parts;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Explosion : Objeto
    {
        FirstExplosion firstExplosion;
        SecondExplosion secondExplosion;
        ThirdExplosion thirdExplosion;
        FourthExplosion fourthExplosion;
        FifthExplosion fifthExplosion;
        SixthExplosion sixthExplosion;
        SeventhExplosion seventhExplosion;
        EighthExplosion EighthExplosion;
        public int duration = 0;
        public bool start = false;
        public Explosion()
        {
            key = "explosion";
            obj = false;
            texture = new Texture("Recursos/explosion6.png");
            init();
        }

        public void init()
        {
            scale = new Vector3(0.8f, 0.8f, 0.8f);
            firstExplosion = new FirstExplosion();
            secondExplosion = new SecondExplosion();
            thirdExplosion = new ThirdExplosion();
            fourthExplosion = new FourthExplosion();
            fifthExplosion = new FifthExplosion();
            sixthExplosion = new SixthExplosion();
            seventhExplosion = new SeventhExplosion();
            EighthExplosion = new EighthExplosion();
            addPart(firstExplosion);
            addPart(secondExplosion);
            addPart(thirdExplosion);
            addPart(fourthExplosion);
            addPart(fifthExplosion);
            addPart(sixthExplosion);
            addPart(seventhExplosion);
            addPart(EighthExplosion);
        }
        public override void CalculateMatrix()
        {
            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void addPart(Parte part)
        {
            parts.Add(part.key, part);
            partCount++;
        }
    }
}
