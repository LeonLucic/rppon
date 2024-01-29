using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DekoraterExtraZadatak
{
    public interface IMealDecorator
    {
        void AddIngredient();
    }

    public class MushroomDecorator : IMealDecorator
    {
        private readonly IMealDecorator _meal;

        public MushroomDecorator(IMealDecorator meal)
        {
            _meal = meal;
        }

        public void AddIngredient()
        {
            _meal.AddIngredient();
            Console.WriteLine("Add Mushrooms");
        }
    }

    public class BeefDecorator : IMealDecorator
    {
        private readonly IMealDecorator _meal;

        public BeefDecorator(IMealDecorator meal)
        {
            _meal = meal;
        }

        public void AddIngredient()
        {
            _meal.AddIngredient();
            Console.WriteLine("Add Beef");
        }
    }

    public class NoodlesDecorator : IMealDecorator
    {
        private readonly IMealDecorator _meal;

        public NoodlesDecorator(IMealDecorator meal)
        {
            _meal = meal;
        }

        public void AddIngredient()
        {
            _meal.AddIngredient();
            Console.WriteLine("Add Noodles");
        }
    }

    public class DecoratedMeal : IMealDecorator
    {
        public void AddIngredient()
        {
            Console.WriteLine("Add Water");
        }
    }

    public static class ClientCode
    {
        public static void Run()
        {
            IMealDecorator decoratedMeal = new NoodlesDecorator(new DecoratedMeal());
            decoratedMeal.AddIngredient();
        }
    }
}





