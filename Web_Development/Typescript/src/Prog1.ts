const numbers: number[] = [1, 2, 3, 4, 5];
const totalOfNumbers: number = numbers.reduce((runningTotal, currentValue) => runningTotal + currentValue, 0);

console.log(`Prog1 executed. Sum of numbers is ${totalOfNumbers}.`);
