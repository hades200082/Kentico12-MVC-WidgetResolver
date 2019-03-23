# Ways to contribute 

There are many different ways in which you can contribute. One of the easiest ways is simply to use our software and provide us with your feedback through the github [issue tracker](../../issues). 

You can also help us improve the project by [sending a pull request](#submitting-pull-requests) with code and documentation changes.

## Where to get support / I found a bug

Please submit issues on the github issue tracker. Alternatively, if you have a fix yourself, please [send a pull request](#submitting-pull-requests).

### I need help with using the projects and/or coding

Please review the README. If you still need help, try the Kentico Development slack channel.

### I have an idea for a new feature (or feedback on existing functionality)

Everybody loves new features! You can submit a new [feature request](../../issues) or you can code it on your own and [send a pull request](#submitting-pull-requests). In either case, don't forget to mention what's the use case and what's the expected output.


## Submitting pull requests

Unless you're fixing a typo, it's usually a good idea to discuss the feature before you submit a pull request with code changes, so let's start with submitting a new [GitHub issue](../../issues) and discussing the whether it fits the vision of a given project.

You might also read these two blogs posts on contributing code: [Open Source Contribution Etiquette](http://tirania.org/blog/archive/2010/Dec-31.html) by Miguel de Icaza and [Don't "Push" Your Pull Requests](https://www.igvita.com/2011/12/19/dont-push-your-pull-requests/) by Ilya Grigorik. Note that all code submissions will be rigorously reviewed and tested, and only those that meet a high bar for quality and design/roadmap appropriateness will be merged into the source.


### Example - process of contribution
If not stated otherwise, we use [feature branch workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow). 

To start with coding, fork the repository you want to contribute to, create a new branch, and start coding. Once the functionality is [done](#Definition-of-Done), you can submit a [pull request](https://help.github.com/articles/about-pull-requests/). 

### Definition of Done

- New/fixed code is covered with tests
- CI can build the code
- All tests are pass
- New version number follows [semantic versioning](https://semver.org/)
- Coding style (spaces, indentation) is in line with the rest of the code in a given repository
- Documentation is updated (e.g. code examples in README, Wiki pages, etc.)
- All `public` members are documented (using XML doc, phpdoc, etc.)
- Code doesn't contain any secrets (private keys, etc.)
- Commit messages are clear. Please read these articles: [Writing good commit messages](https://github.com/erlang/otp/wiki/Writing-good-commit-messages), [A Note About Git Commit Messages](https://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html), [On commit messages](https://who-t.blogspot.com/2009/12/on-commit-messages.html)


### Feedback

Your pull request will now go through extensive checks by the subject matter experts on our team. Please be patient. Update your pull request according to feedback until it is approved by one of the Kentico maintainers. After that, one of our team members may adjust the branch you merge into based on the expected release schedule.

### Conduct

All contributors are expected to adhere to Kentico's [Code of conduct](https://github.com/Kentico/devnet.kentico.com/blob/master/CODE_OF_CONDUCT.md)