Here is a simple console application in TypeScript that implements an autocomplete system with frequency-based ranking:

```typescript
class AutocompleteSystem {
    private frequencyMap: Map<string, number>;
    private input: string;

    constructor(sentences: string[], times: number[]) {
        this.frequencyMap = new Map();
        this.input = '';

        for (let i = 0; i < sentences.length; i++) {
            this.frequencyMap.set(sentences[i], times[i]);
        }
    }

    inputChar(c: string): string[] {
        if (c === '#') {
            this.frequencyMap.set(this.input, (this.frequencyMap.get(this.input) || 0) + 1);
            this.input = '';
            return [];
        }

        this.input += c;
        let suggestions = Array.from(this.frequencyMap.keys()).filter(key => key.startsWith(this.input));
        suggestions.sort((a, b) => {
            if (this.frequencyMap.get(b) !== this.frequencyMap.get(a)) {
                return this.frequencyMap.get(b) - this.frequencyMap.get(a);
            } else {
                return a < b ? -1 : 1;
            }
        });

        return suggestions.slice(0, 3);
    }
}

let autocompleteSystem = new AutocompleteSystem(["i love you", "island", "i love leetcode"], [5, 3, 2]);
console.log(autocompleteSystem.inputChar('i')); // ["i love you", "island", "i love leetcode"]
console.log(autocompleteSystem.inputChar(' ')); // ["i love you", "i love leetcode"]
console.log(autocompleteSystem.inputChar('a')); // []
console.log(autocompleteSystem.inputChar('#')); // []
```

This application uses a `Map` to store sentences and their frequencies. The `inputChar` method updates the current input and returns the top 3 suggestions based on the frequency and lexicographical order. If the input character is '#', it means the current input is finished and its frequency is increased by 1.