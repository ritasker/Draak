namespace TowerOfDelusion.Build;

public class GithubCICDPredefinedVariables
{
    /// <summary> Always set to true. </summary> 
    public string? CI { get; set; }

    /// <summary>
    /// The name of the action currently running, or the id of a step. For example, for an action, __repo-owner_name-of-action-repo.
    /// GitHub removes special characters, and uses the name __run when the current step runs a script without an id.If you
    /// use the same script or action more than once in the same job, the name will include a suffix that consists of
    /// the sequence number preceded by an underscore.For example, the first script you run will have the name
    /// _run, and the second script will be named __run_2.Similarly, the second invocation of actions/checkout will be
    /// actionscheckout2. 
    ///</summary>
    public string? GITHUB_ACTION { get; set; }

    /// <summary> The path where an action is located. This property is only supported in composite actions. You can use this path to access files located in the same repository as the action. For example, /home/runner/work/_actions/repo-owner/name-of-action-repo/v1. </summary> 
    public string? GITHUB_ACTION_PATH { get; set; }

    /// <summary> For a step executing an action, this is the owner and repository name of the action. For example, actions/checkout. </summary> 
    public string? GITHUB_ACTION_REPOSITORY { get; set; }

    /// <summary> Always set to true when GitHub Actions is running the workflow. You can use this variable to differentiate when tests are being run locally or by GitHub Actions. </summary> 
    public string? GITHUB_ACTIONS { get; set; }

    /// <summary> The name of the person or app that initiated the workflow. For example, octocat. </summary> 
    public string? GITHUB_ACTOR { get; set; }

    /// <summary> The account ID of the person or app that triggered the initial workflow run. For example, 1234567. Note that this is different from the actor username. </summary> 
    public string? GITHUB_ACTOR_ID { get; set; }

    /// <summary> Returns the API URL. For example: https://api.github.com. </summary> 
    public string? GITHUB_API_URL { get; set; }

    /// <summary> The name of the base ref or target branch of the pull request in a workflow run. This is only set when the event that triggers a workflow run is either pull_request or pull_request_target. For example, main. </summary> 
    public string? GITHUB_BASE_REF { get; set; }

    /// <summary> The path on the runner to the file that sets variables from workflow commands. This file is unique to the current step and changes for each step in a job. For example, /home/runner/work/_temp/_runner_file_commands/set_env_87406d6e-4979-4d42-98e1-3dab1f48b13a. For more information, see "Workflow commands for GitHub Actions." </summary> 
    public string? GITHUB_ENV { get; set; }

    /// <summary> The name of the event that triggered the workflow. For example, workflow_dispatch. </summary> 
    public string? GITHUB_EVENT_NAME { get; set; }

    /// <summary> The path to the file on the runner that contains the full event webhook payload. For example, /github/workflow/event.json. </summary> 
    public string? GITHUB_EVENT_PATH { get; set; }

    /// <summary> Returns the GraphQL API URL. For example: https://api.github.com/graphql. </summary> 
    public string? GITHUB_GRAPHQL_URL { get; set; }

    /// <summary> The head ref or source branch of the pull request in a workflow run. This property is only set when the event that triggers a workflow run is either pull_request or pull_request_target. For example, feature-branch-1. </summary> 
    public string? GITHUB_HEAD_REF { get; set; }

    /// <summary> The job_id of the current job. For example, greeting_job. </summary> 
    public string? GITHUB_JOB { get; set; }

    /// <summary> The path on the runner to the file that sets system PATH variables from workflow commands. This file is unique to the current step and changes for each step in a job. For example, /home/runner/work/_temp/_runner_file_commands/add_path_899b9445-ad4a-400c-aa89-249f18632cf5. For more information, see "Workflow commands for GitHub Actions." </summary> 
    public string? GITHUB_PATH { get; set; }

    /// <summary> The fully-formed ref of the branch or tag that triggered the workflow run. For workflows triggered by push, this is the branch or tag ref that was pushed. For workflows triggered by pull_request, this is the pull request merge branch. For workflows triggered by release, this is the release tag created. For other triggers, this is the branch or tag ref that triggered the workflow run. This is only set if a branch or tag is available for the event type. The ref given is fully-formed, meaning that for branches the format is refs/heads/<branch_name>, for pull requests it is refs/pull/<pr_number>/merge, and for tags it is refs/tags/<tag_name>. For example, refs/heads/feature-branch-1. </summary> 
    public string? GITHUB_REF { get; set; }

    /// <summary> The short ref name of the branch or tag that triggered the workflow run. This value matches the branch or tag name shown on GitHub. For example, feature-branch-1. </summary> 
    public string? GITHUB_REF_NAME { get; set; }

    /// <summary> true if branch protections are configured for the ref that triggered the workflow run. </summary> 
    public string? GITHUB_REF_PROTECTED { get; set; }

    /// <summary> The type of ref that triggered the workflow run. Valid values are branch or tag. </summary> 
    public string? GITHUB_REF_TYPE { get; set; }

    /// <summary> The owner and repository name. For example, octocat/Hello-World. </summary> 
    public string? GITHUB_REPOSITORY { get; set; }

    /// <summary> The ID of the repository. For example, 123456789. Note that this is different from the repository name. </summary> 
    public string? GITHUB_REPOSITORY_ID { get; set; }

    /// <summary> The repository owner's name. For example, octocat. </summary> 
    public string? GITHUB_REPOSITORY_OWNER { get; set; }

    /// <summary> The repository owner's account ID. For example, 1234567. Note that this is different from the owner's name. </summary> 
    public string? GITHUB_REPOSITORY_OWNER_ID { get; set; }

    /// <summary> The number of days that workflow run logs and artifacts are kept. For example, 90. </summary> 
    public string? GITHUB_RETENTION_DAYS { get; set; }

    /// <summary> A unique number for each attempt of a particular workflow run in a repository. This number begins at 1 for the workflow run's first attempt, and increments with each re-run. For example, 3. </summary> 
    public string? GITHUB_RUN_ATTEMPT { get; set; }

    /// <summary> A unique number for each workflow run within a repository. This number does not change if you re-run the workflow run. For example, 1658821493. </summary> 
    public string? GITHUB_RUN_ID { get; set; }

    /// <summary> A unique number for each run of a particular workflow in a repository. This number begins at 1 for the workflow's first run, and increments with each new run. This number does not change if you re-run the workflow run. For example, 3. </summary> 
    public string? GITHUB_RUN_NUMBER { get; set; }

    /// <summary> The URL of the GitHub server. For example: https://github.com. </summary> 
    public string? GITHUB_SERVER_URL { get; set; }

    /// <summary> The commit SHA that triggered the workflow. The value of this commit SHA depends on the event that triggered the workflow. For more information, see "Events that trigger workflows." For example, ffac537e6cbbf934b08745a378932722df287a53. </summary> 
    public string? GITHUB_SHA { get; set; }

    /// <summary> The path on the runner to the file that contains job summaries from workflow commands. This file is unique to the current step and changes for each step in a job. For example, /home/runner/_layout/_work/_temp/_runner_file_commands/step_summary_1cb22d7f-5663-41a8-9ffc-13472605c76c. For more information, see "Workflow commands for GitHub Actions." </summary> 
    public string? GITHUB_STEP_SUMMARY { get; set; }

    /// <summary> The name of the workflow. For example, My test workflow. If the workflow file doesn't specify a name, the value of this variable is the full path of the workflow file in the repository. </summary> 
    public string? GITHUB_WORKFLOW { get; set; }

    /// <summary> The ref path to the workflow. For example, octocat/hello-world/.github/workflows/my-workflow.yml@refs/heads/my_branch. </summary> 
    public string? GITHUB_WORKFLOW_REF { get; set; }

    /// <summary> The commit SHA for the workflow file. </summary> 
    public string? GITHUB_WORKFLOW_SHA { get; set; }

    /// <summary> The default working directory on the runner for steps, and the default location of your repository when using the checkout action. For example, /home/runner/work/my-repo-name/my-repo-name. </summary> 
    public string? GITHUB_WORKSPACE { get; set; }

    /// <summary> The architecture of the runner executing the job. Possible values are X86, X64, ARM, or ARM64. </summary> 
    public string? RUNNER_ARCH { get; set; }

    /// <summary> This is set only if debug logging is enabled, and always has the value of 1. It can be useful as an indicator to enable additional debugging or verbose logging in your own job steps. </summary> 
    public string? RUNNER_DEBUG { get; set; }

    /// <summary> The name of the runner executing the job. For example, Hosted Agent </summary> 
    public string? RUNNER_NAME { get; set; }

    /// <summary> The operating system of the runner executing the job. Possible values are Linux, Windows, or macOS. For example, Windows </summary> 
    public string? RUNNER_OS { get; set; }

    /// <summary> The path to a temporary directory on the runner. This directory is emptied at the beginning and end of each job. Note that files will not be removed if the runner's user account does not have permission to delete them. For example, D:\a\_temp </summary> 
    public string? RUNNER_TEMP { get; set; }

    /// <summary> The path to the directory containing preinstalled tools for GitHub-hosted runners. For more information, see "About GitHub-hosted runners". For example, C:\hostedtoolcache\windows </summary> 
    public string? RUNNER_TOOL_CACHE { get; set; }
}