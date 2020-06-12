# Karting Microgame

This is a developer readme. For the end user readme, please see `Packages/com.unity.template.kart/README.md`.

The target Unity version for this project is currently 2019.3. The most important target platform for the project
is WebGL, so make sure you are running it as the target platform on a regular basis.

## Microgame Add-Ons

If this Microgame has add-ons, they can be found from `/Assets/AddOns`. The add-ons can be obtained for development
using `git submodule update --init --recursive`, or, if doing a fresh clone, use the `--recursive` switch when cloning.
Use `git submodule update --remote` to get the latest commit for the add-ons, submodule reference update commits are not
 required as the submodule should track the `master` branch automatically. `export-addons.bat` can be used to export
the add-ons, however, preferably fetch them from the [Export Add-Ons pipeline]. See more information about the add-ons'
development in the add-ons' readme.

## Testing

`staging` can be used by Q&A and other testers to do some preliminary testing.
However, **the proper testing should be done using the Microgame Template and Asset Store packages**.

### Testing Microgame as a template

1. Either download `packages` artifact that was created from the latest commit from the project's
[Pack pipeline], or if a version was already published to the [Candidates repository], use it and go to step 6.
1. To package the Microgame yourself, make sure you have [Node.js](https://nodejs.org/en/) installed and `npm` on your `PATH`.
1. Install the `upm-ci` package globally: ```npm install upm-ci-utils -g --registry https://api.bintray.com/npm/unity/unity-npm```
1. Open a console/terminal window and `cd` your way inside the Microgame project folder.
1. Run `build.bat`/`build.sh`: this will generate a folder `/upm-ci~/templates/` containing a `.tgz` file of your converted template.
   - By default, the build script will delete extra asset folders only if they are empty. Pass in `force` as an argument
   to force the deletion always. **Make sure you don't have any uncommitted work done for the extra assets if using this argument.**
1. Copy the packaged template to one of these paths to make it available in the Editor when creating new projects:
   - Windows: `<Unity Editor Root>/Data/Resources/PackageManager/ProjectTemplates`
   - macOS: `<Unity Editor Root>/Contents/Resources/PackageManager/ProjectTemplates`
1. Open Unity Hub. Locate the Editor to which you added your template to. When creating a new project, you should see
your template in the templates list.

### Testing the Asset Store use case

1. Create the Microgame template package as described above.
1. Create a new project using the package.
1. Use Asset Store Tools.
1. TODO Preferably we would test the actual .unitypackages we submitted to Asset Store. These are stored in somewhere, find out where.

## Dependencies

Any dependency in `Packages/manifest.json` is a development dependency (is *not* included in the Microgame template package)
and becomes a production dependency (*is* included in the Microgame template package) only if the dependency is also added to
`manifest-prod.json` and `package.json` when necessary (needed for code compilation or end-user UX, or is not a default dependency
of new Unity projects, ). The CI will replace `Packages/manifest.json` with the production version before packing the template.
The dependencies' versions in `package.json` must match the versions in `manifest.json` (and `manifest-prod.json`), otherwise the
Package Validation Suite will fail. It's a good idea to run Package Validation Suite locally (it should be a dev dependency of the
project) locally before committing any dependency changes.

## Branches

- `master` — "stable", latest release
  - the latest release of `com.unity.learn.iet-framework` and `com.unity.connect.share` used (`manifest.json` and `package.json`)
- `dev` — current Microgame development, the same dependencies as in `master`
- `dev-iet` — current Microgame **and** IET-related development, the following packages used as local packages:
  - `file:../../iet-package/Packages/com.unity.learn.iet-framework`
  - `file:../../iet-package/Packages/com.unity.learn.iet-framework.authoring`
  - `file:../../com.unity.connect.share`
  - `package.json` does not refer to IET dependencies (local dependencies not supported)
  - this branch is **not** meant to be packaged as a template, only use it for local development of new IET & Share features
- `staging` — `com.unity.learn.iet-framework` and `com.unity.connect.share` used from  
  `"registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"` (`manifest.json`)
  - requires the user to be in Unity's network, remove registery reference and use the latest releases, if you don't have
    access to the network (`manifest.json`)

**Remember to pay attention that `manifest.json`, `manifest-prod.json`, and `package.json` are in order after merging branches.**

## Making a release

When beginning the release process, the commits flow as follows: `dev` -> `dev-iet` (if new features that depend on new packages
are developed simultaneously)-> `staging`. After the release is done, remove the potential candidates repo references from the
manifests (see _Dependencies_), publish potential new packages, commit, wait for the package to be ready and publish it. After
this, merge `staging` to `master` and tag the latest commit with the version number. See _Brances_ for more information.

When making a release, make sure the following files in `Packages/com.unity.template.<name>` folder are up-to-date:
- `package.json`
- `CHANGELOG.md`
- `README.md` (remember to update the version and changelog in this file also)
- `Third Party Notices.md` (if applicable)
- `Documentation~/<Name of the Microgame>.md`
- `ProjectSettings/ProjectSettings.asset`: make sure that `PlayerSettings`' _Bundle Identifier_
(_PC, Mac & Linux Standalone settings > Other Settings_) and _Version_ match the `name` and `version` in `package.json`. 
- also check that the year is correct in `LICENSE.md` at the beginning of a new year

### Submitting the Microgame and Microgame Add-Ons to Asset Store

Microgames are also distributed via the Asset Store so that they are obtainable in Hub's Learn tab.

1. Obtain the Microgame template package and create a new project using the package as described above.
1. Test the Microgame as usual.
1. Obtain Asset Store Tools and submit the Microgame to Asset Store.
1. Import and test the add-ons one by one. Each of the add-ons must work on their own and not cause any conflicts with the other add-ons.
1. Submit the add-ons to Asset Store. Pay attention that all the assets submitted should be under the specific add-on's folder.

## Automated builds and tests

See [Yamato]. CI is run for each commmit, excluding `master` branch. Note that Yamato is currently only accessible in Unity's network.
<!-- Note: make sure that the pipeline links point to staging -->
[Export Add-Ons pipeline]: https://yamato.prd.cds.internal.unity3d.com/jobs/467-Karting%2520Microgame/tree/staging/.yamato%252Fupm-ci.yml%2523export_addons
[Pack pipeline]: https://yamato.prd.cds.internal.unity3d.com/jobs/467-Karting%2520Microgame/tree/staging/.yamato%252Fupm-ci.yml%2523pack
[Candidates repository]: https://artifactory.prd.cds.internal.unity3d.com/artifactory/upm-candidates/com.unity.template.kart/-/
[Yamato]: https://yamato.prd.cds.internal.unity3d.com/jobs/467-Karting%2520Microgame
