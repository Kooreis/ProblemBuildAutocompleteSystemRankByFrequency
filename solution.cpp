```cpp
#include <iostream>
#include <vector>
#include <unordered_map>
#include <algorithm>

using namespace std;

class AutocompleteSystem {
public:
    struct TrieNode {
        unordered_map<char, TrieNode*> children;
        string str;
        int count;
        TrieNode(): str(""), count(0) {}
    };

    struct comp {
        bool operator() (pair<string, int>& a, pair<string, int>& b) {
            return a.second > b.second || (a.second == b.second && a.first < b.first);
        }
    } compare;

    AutocompleteSystem(vector<string>& sentences, vector<int>& times) {
        root = new TrieNode();
        for (int i = 0; i < sentences.size(); i++) {
            insert(root, sentences[i], times[i]);
        }
    }

    vector<string> input(char c) {
        if (c != '#') {
            cur += c;
            return search(root, cur);
        } else {
            insert(root, cur, 1);
            cur = "";
        }
        return {};
    }

private:
    void insert(TrieNode* root, string s, int times) {
        TrieNode* cur = root;
        for (int i = 0; i < s.size(); i++) {
            if (!cur->children.count(s[i])) {
                cur->children[s[i]] = new TrieNode();
            }
            cur = cur->children[s[i]];
        }
        cur->count += times;
        cur->str = s;
    }

    vector<string> search(TrieNode* root, string s) {
        TrieNode* cur = root;
        for (int i = 0; i < s.size(); i++) {
            if (!cur->children.count(s[i])) {
                return {};
            }
            cur = cur->children[s[i]];
        }
        vector<pair<string, int>> res;
        dfs(cur, res);
        sort(res.begin(), res.end(), compare);
        vector<string> top3;
        for (int i = 0; i < min(3, (int)res.size()); i++) {
            top3.push_back(res[i].first);
        }
        return top3;
    }

    void dfs(TrieNode* root, vector<pair<string, int>>& res) {
        if (root->str != "") {
            res.push_back({root->str, root->count});
        }
        for (auto& kv : root->children) {
            dfs(kv.second, res);
        }
    }

    TrieNode* root;
    string cur = "";
};

int main() {
    vector<string> sentences = {"i love you", "island", "iroman", "i love leetcode"};
    vector<int> times = {5, 3, 2, 2};
    AutocompleteSystem* obj = new AutocompleteSystem(sentences, times);
    vector<string> param_1 = obj->input('i');
    for (auto& word : param_1) {
        cout << word << endl;
    }
    return 0;
}
```