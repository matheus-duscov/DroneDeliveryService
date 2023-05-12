# DRONE DELIVERY SERVICE CODING TEST

## Summary

A squad of drones is tasked with delivering packages for a major online retailer in a
world where time and distance do not matter.

Each drone can carry a specific weight and can make multiple deliveries before
returning to home base to pick up additional packages; however, the goal is to make
the fewest number of trips, as each time the drone returns to home base, it is
extremely costly to refuel and reload the drone.

The software shall accept input which will include: the name of each drone, the
maximum weight it can carry, along with a series of locations and the total weight
needed to be delivered to that specific location. The software should highlight the most
efficient deliveries for each drone to make on each trip.

Assume that time and distance to each location do not matter, and that the size of
each package is irrelevant. It is also assumed that the cost to refuel and restock each
drone is a constant and does not vary between drones.

The maximum number of drones in a squad is 100, and there is no maximum number
of required deliveries.

## Approach Used

- First I sort the drone list and the location list by decrescent weight / package weight.
- Then for each drone, I give the location with the highest package weight possible.
- If the drone maximum capacity is not reached yet, I add another package, considering the drone's remaining weight capacity, repeating until there is no compatible package to add.
- If none location package weight is added, move to fill the next drone.

## Technical Depedencies and Libraries

Built with .NET 6 and Visual Studio 2022.