using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica.ObjectController
{
    class OPlanner
    {
        OExecutor oExecutor;
        //List<Bullet> bullets = new List<Bullet>();
        Bullet[] bullets = new Bullet[20];
        Explosion[] explosions = new Explosion[20];
        public int bulletCount;
        public OPlanner()
        {
            bulletCount = 0;
            oExecutor = new OExecutor();
        }

        public void addBullet(Hashtable objects, Tank tank)
        {
            Bullet bullet = new Bullet();
            Explosion explosion = new Explosion();
            switch (tank.movement.direction)
            {
                case Movement.Directions.PlusZ:
                    bullet.center = tank.center + new Vector3(0.0f, 0.0f, tank.movement.radius);
                    bullet.translation = tank.translation + new Vector3(0.0f, 0.2f, tank.movement.radius);
                    break;
                case Movement.Directions.MinusX:
                    bullet.center = tank.center + new Vector3(-tank.movement.radius, 0.0f, 0.0f);
                    bullet.translation = tank.translation + new Vector3(-tank.movement.radius, 0.2f, 0.0f);
                    break;
                case Movement.Directions.MinusZ:
                    bullet.center = tank.center + new Vector3(0.0f, 0.0f, -tank.movement.radius);
                    bullet.translation = tank.translation + new Vector3(0.0f, 0.2f, -tank.movement.radius);
                    break;
                case Movement.Directions.PlusX:
                    bullet.center = tank.center + new Vector3(tank.movement.radius, 0.0f, 0.0f);
                    bullet.translation = tank.translation + new Vector3(tank.movement.radius, 0.2f, 0.0f);
                    break;
            }
            bullet.movement.direction = tank.movement.direction;
            bulletCount++;
            bullet.key = "bullet" + bulletCount;
            explosion.key = "explosion" + bulletCount;
            bullet.parentKey = tank.key;
            explosion.parentKey = bullet.key;
            objects.Add(bullet.key, bullet);
            objects.Add(explosion.key, explosion);
            bullets[bulletCount - 1] = bullet;
            explosions[bulletCount - 1] = explosion;
            Console.WriteLine("Lista: " + objects.Count);
        }

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            oExecutor.setWalls(vertical, horizontal);
        }

        public void run(Tank firstTank, Tank secondTank)
        {
            oExecutor.run(bullets, explosions, firstTank, secondTank);
        }

        public void dispose()
        {
            oExecutor.running = false;
        }
    }
}
