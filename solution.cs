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
}