Here is a simple console application in JavaScript that implements an autocomplete system. It uses a Trie data structure for efficient search and retrieval of words. The Trie nodes also store a frequency count for each word to rank suggestions by frequency of use.

```javascript
class TrieNode {
    constructor() {
        this.children = {};
        this.endOfWord = false;
        this.count = 0;
    }
}

class Trie {
    constructor() {
        this.root = new TrieNode();
    }

    insert(word) {
        let current = this.root;
        for (let i = 0; i < word.length; i++) {
            let ch = word.charAt(i);
            let node = current.children[ch];
            if (node == null) {
                node = new TrieNode();
                current.children[ch] = node;
            }
            current = node;
        }
        current.endOfWord = true;
        current.count++;
    }

    search(word) {
        let current = this.root;
        for (let i = 0; i < word.length; i++) {
            let node = current.children[word.charAt(i)];
            if (node == null) {
                return false;
            }
            current = node;
        }
        return current.endOfWord;
    }

    delete(word) {
        this.deleteRecursively(this.root, word, 0);
    }

    deleteRecursively(current, word, index) {
        if (index == word.length) {
            if (!current.endOfWord) {
                return false;
            }
            current.endOfWord = false;
            return Object.keys(current.children).length == 0;
        }
        let ch = word.charAt(index), node = current.children[ch];
        if (node == null) {
            return false;
        }
        let shouldDeleteCurrentNode = this.deleteRecursively(node, word, index + 1);
        if (shouldDeleteCurrentNode) {
            delete current.children[ch];
            return Object.keys(current.children).length == 0;
        }
        return false;
    }

    suggest(word) {
        let current = this.root;
        for (let i = 0; i < word.length; i++) {
            let node = current.children[word.charAt(i)];
            if (node == null) {
                return [];
            }
            current = node;
        }
        return this.suggestionsRecursively(current, word);
    }

    suggestionsRecursively(node, prefix) {
        if (node.endOfWord) {
            return [{ word: prefix, count: node.count }];
        }
        let suggestions = [];
        for (let child in node.children) {
            suggestions = suggestions.concat(this.suggestionsRecursively(node.children[child], prefix + child));
        }
        return suggestions.sort((a, b) => b.count - a.count);
    }
}

let trie = new Trie();
trie.insert('hello');
trie.insert('hello');
trie.insert('hell');
trie.insert('heaven');
trie.insert('heaven');
trie.insert('heaven');
console.log(trie.suggest('he'));
```

This console application first creates a Trie and inserts some words into it. The `insert` method increases the count of a word each time it is inserted. The `suggest` method returns a list of suggestions for a given prefix, sorted by frequency of use. The `suggestionsRecursively` method is a helper method that recursively finds all words in the Trie that start with a given prefix.