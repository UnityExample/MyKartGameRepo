using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

class ContentTests
{
    [Test]
    public void AtLeastOnePostProcessVolumeExistsAndIsEnabled()
    {
        var ppv = Object.FindObjectsOfType<PostProcessVolume>().FirstOrDefault();
        Assert.True(ppv != null, "No PostProcessVolume component found in the scene.");

        Assert.True(ppv.enabled, "PostProcessVolume component is not enabled.");
    }
}
