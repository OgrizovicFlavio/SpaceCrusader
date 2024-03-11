using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPJ_SpaceCrusader_OgrizovicFlavio
{
    static class CollisionsHandler
    {
        static List<GameObjectBase> _entities = new List<GameObjectBase>();

        static List<GameObjectBase> _entitiesToRemove = new List<GameObjectBase>();

        static List<KeyValuePair<GameObjectBase, GameObjectBase>> collisionRegister = new List<KeyValuePair<GameObjectBase, GameObjectBase>>();
        public static void FlagEntityForRemoval(GameObjectBase entity)
        {
            if (_entitiesToRemove.Contains(entity))
            {
                Console.WriteLine("El Handler ya posee esta entidad");
            }
            else _entitiesToRemove.Add(entity);
        }
        public static void RemoveFlaggedEntities()
        {
            for (int i = 0; i < _entitiesToRemove.Count; i++)
            {
                if (!_entities.Contains(_entitiesToRemove[i]))
                {
                    Console.WriteLine("El Handler no posee esta entidad");
                }
                else _entities.Remove(_entitiesToRemove[i]);
            }
            _entitiesToRemove.Clear();
        }
        public static void Add(GameObjectBase entity)
        {
            if (_entities.Contains(entity))
            {
                Console.WriteLine("El CollisionHandler ya posee esta entidad");
            }
            else _entities.Add(entity);
        }

        public static void Remove(GameObjectBase entity)
        {
            if (!_entities.Contains(entity))
            {
                Console.WriteLine("El CollisionHandler no posee esta entidad");
            }
            else _entities.Remove(entity);
        }

        public static void ResolveCollisions(GameObjectBase first, GameObjectBase second)
        {
            FloatRect firstBounds = first.GetSprite().GetGlobalBounds();
            FloatRect secondBounds = second.GetSprite().GetGlobalBounds();

            first.SolveCollision(second);
            second.SolveCollision(first);
        }

        public static void Update()
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                for (int j = i + 1; j < _entities.Count; j++)
                {
                    KeyValuePair<GameObjectBase, GameObjectBase> register = new KeyValuePair<GameObjectBase, GameObjectBase>(_entities[i], _entities[j]);

                    if (_entities[i].IsColliding(_entities[j]))
                    {
                        ResolveCollisions(_entities[i], _entities[j]);

                        if (!collisionRegister.Contains(register))
                        {
                            _entities[i].OnCollisionEnter(_entities[j]);
                            _entities[j].OnCollisionEnter(_entities[i]);
                            collisionRegister.Add(register);
                        }
                    }
                    else
                    {
                        if(collisionRegister.Contains(register))
                        {
                            _entities[i].OnCollisionExit(_entities[j]);
                            _entities[j].OnCollisionExit(_entities[i]);
                            collisionRegister.Remove(register);
                        }
                    }
                }
            }
            RemoveFlaggedEntities();
        }
    }
}
