using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge;

namespace ElevatorChallengeUnitTest
{
    public class ElevatorSystemTests
    {
        [Fact]
        public void ElevatorSystem_CallsNearestElevator()
        {
            // Arrange
            var elevatorSystem = new ElevatorSystem(3, 10);
            var elevator = elevatorSystem.GetElevator(1);
            var initialFloor = elevator.CurrentFloor;
            var initialNumPeople = elevator.NumPeople;

            // Act
            elevatorSystem.CallElevator(5, 3);
            elevator = elevatorSystem.GetElevator(1); // Reassign the elevator object

            // Assert
            Assert.Equal(Direction.Up, elevator.CurrentDirection);
            Assert.True(elevator.IsMoving);
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal(initialNumPeople + 3, elevator.NumPeople);

        }


        [Fact]
        public void ElevatorSystem_FullCapacity()
        {
            // Arrange
            int numElevators = 1;
            int elevatorCapacity = 3;
            ElevatorSystem elevatorSystem = new ElevatorSystem(numElevators, elevatorCapacity);

            // Set the initial position of the elevator
            elevatorSystem.GetElevator(1).CurrentFloor = 1;

            int targetFloor = 3;

            // Act
            elevatorSystem.CallElevator(targetFloor, elevatorCapacity); // Fill the elevator to full capacity

            // Assert
            Assert.Equal(targetFloor, elevatorSystem.GetElevator(1).CurrentFloor); // Check the elevator's position
            Assert.Equal(elevatorCapacity, elevatorSystem.GetElevator(1).NumPeople); // Check the elevator's number of people

            // Try to enter more people than the capacity
            elevatorSystem.CallElevator(targetFloor, elevatorCapacity + 1); // Try to add one more person

            // Assert
            Assert.Equal(targetFloor, elevatorSystem.GetElevator(1).CurrentFloor); // The elevator should remain at the same floor
            Assert.Equal(elevatorCapacity, elevatorSystem.GetElevator(1).NumPeople); // The number of people in the elevator should remain the same
        }

        [Fact]
        public void ElevatorSystem_AllElevatorsBusy()
        {
            // Arrange
            int numElevators = 3;
            int elevatorCapacity = 10;
            ElevatorSystem elevatorSystem = new ElevatorSystem(numElevators, elevatorCapacity);

            // Set all elevators to be busy
            foreach (var elevator in elevatorSystem.GetElevators())
            {
                elevator.IsMoving = true;
            }

            int floor = 5;
            int numPeople = 3;

            // Act
            elevatorSystem.CallElevator(floor, numPeople);

            // Assert
            Assert.True(elevatorSystem.AllElevatorsBusy());
        }
    }
}
