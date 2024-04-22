
# Dev Notes

  

## About the Implementation

  

The code was refactored according to the given specifications. In my opinion, the central aspect was how the processing of the various rebates was implemented in a solid and efficient manner. I opted for the `strategy` pattern, as it seemed to be the option that would most easily allow for adding new types of rebates.

  

Next, there must be a repository for the strategies so that the code can find the correct strategy to implement. The code interacts with an `IRebateStrategyCalculationService`, which returns the appropriate strategy for the type of incentive. The issue is that the code implementing this class is merely a class with a dictionary containing an entry for each strategy. 
This is far from being a good solution, as more sophisticated mechanisms such as having this registry in a database, a configuration file, or what I consider the best approach, using reflection to automatically find and register the strategies, are usually employed. 
Given that part of the requirements was to spend approximately 1 hour implementing the changes, I applied the mentioned solution, but if more time were available, I would have preferred this last option.

  

To add a new type of rebate, the changes to apply are:

  

1. Modify the `IncentiveType` and `SupportedIncentiveType` enums to include the new type of Rebate.

2. Create a new class in `Smartwyre.DeveloperTest\Utils\Rebates`, inheriting from `RebateCalculationStrategy`. This class must implement one method and two abstract properties.

3. Modify the `StaticRebateStrategyCalculationService` class to add a new mapping between the new type of incentive and its Strategy class.

  

With these steps, the code should function correctly.

If you want to add tests for the new class, the mechanism is similar. 
A new class that inherits from `RebateCalculationStrategyBaseTests` and implements the abstract methods and properties should be created in the test project. 
This alone will implement a series of tests for many functionalities common to all the strategy classes. Only methods to test the processing logic of this new rebate should be added.

  

## Notes about the code

  

- The original code had a minor inconsistency. For all rebates except one, it checked if the product was `null` and returned `false`. I wasn't sure if this was left intentionally because there might be some special business logic that said for that type of rebate the check should not be made (and throw `NullReferenceException` if it happened) or if it was simply an oversight. I interpreted it as the latter option, so in my code, for all rebates, it checks if the product is `null` (just like the rebate).

- If the `Smartwyre.DeveloperTest.Runner` project is executed, you will see that it tries to calculate a rebate, but it returns `False`. This is because the provided implementations of the Product and Rebate data stores return empty objects. If coherent values are placed in them, the calculation works.

- A problem that could be solved in different ways is how to map the `IncentiveType` enumeration to `SupportedIncentiveType` and vice versa. 
While the easiest solution would be to eliminate `IncentiveType`, or make it inherit from `SupportedIncentiveType`, I assumed both were included intentionally to see what strategy was used to map them. It might be thought, for example, that other external systems call our method and work with the current values of the `IncentiveType` enumeration (which would be values 1, 2, and 3), so they cannot be changed.
To perform the mapping, various strategies can be used. My preferred one would be to create a Custom attribute and apply it to the elements of `IncentiveType`, indicating the corresponding value in `SupportedIncentiveType`. For example:
```csharp
public  enum  IncentiveType
{
	[IncentiveMapping(SupportedIncentiveType.FixedRateRebate)]
	FixedRateRebate,
	...
}
```

  

But in this case, I preferred that the mapping be done explicitly in each class that implements a strategy. This is because, by doing it this way, a developer who wants to add a new Rebate must necessarily establish which `IncentiveType` and `SupportedIncentiveType` correspond to that strategy, and thus, even if forgotten, will add the new value in the corresponding enumerations. Using attributes, there is no way to warn that a mapping was missed in an element until the code is in execution.