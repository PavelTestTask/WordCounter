Feature: Word counter
	As an author
	I want to know the number of times each word appears in a sentence
	So that I can make sure I'm not repeating myself

Scenario Outline: Count words in a sentence
	Given a sentence <Sentence>
	When the program is run
	Then I'm returned a distinct list of words in the sentence and the number of times they have occurred
		| Key       | Value |
		| this      | 2     |
		| is        | 2     |
		| a         | 1     |
		| statement | 1     |
		| and       | 1     |
		| so        | 1     |

Examples:
		| Sentence                                      |
		| 'This is a statement, and so is this.'        |
		| '- (This) is a "statement", and so is this?!' |


Scenario: Input text should be a single sentence
	Given a sentence 'This is a statement, and so is this. But this is a text.'
	When the program is run
	Then multiple sentences are not allowed

Scenario: Input text should not be empty
	Given a sentence ''
	When the program is run
	Then empty input is not allowed
