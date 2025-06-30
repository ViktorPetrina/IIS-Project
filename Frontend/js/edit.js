document.getElementById('year').textContent = new Date().getFullYear();

const apiUrl = "http://localhost:5095/api/Specifications";
const token = localStorage.getItem("accessToken") || "";
const urlParams = new URLSearchParams(window.location.search);
const customId = urlParams.get('id');

if (!customId) {
    alert("No specification ID provided.");
    window.location.href = "index.html";
}

const form = document.getElementById('editForm');

async function loadSpecification() {
    try {
        const response = await fetch(`${apiUrl}/${customId}`, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "Accept": "application/xml"
            }
        });
        if (!response.ok) throw new Error("Failed to load specification.");
        const xmlText = await response.text();
        fillForm(xmlText);
    } catch (err) {
        console.error(err);
        alert("Error loading specification.");
    }
}

function fillForm(xmlText) {
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlText, "application/xml");

    for (const field of form.elements) {
        if (field.name) {
            field.value = xmlDoc.querySelector(field.name)?.textContent ?? "";
        }
    }
}

form.addEventListener('submit', async (e) => {
    e.preventDefault();
    const xmlPayload = `
<specification>
    <phoneDetails>
        <yearValue>${form.yearValue.value}</yearValue>
        <brandValue>${form.brandValue.value}</brandValue>
        <modelValue>${form.modelValue.value}</modelValue>
    </phoneDetails>
    <gsmLaunchDetails>
        <launchAnnounced>${form.launchAnnounced.value}</launchAnnounced>
        <launchStatus>${form.launchStatus.value}</launchStatus>
    </gsmLaunchDetails>
    <gsmBodyDetails>
        <bodyDimensions>${form.bodyDimensions.value}</bodyDimensions>
        <bodyWeight>${form.bodyWeight.value}</bodyWeight>
        <bodySim>${form.bodySim.value}</bodySim>
        <bodyBuild>${form.bodyBuild.value}</bodyBuild>
        <bodyOther1>${form.bodyOther1.value}</bodyOther1>
        <bodyOther2>${form.bodyOther2.value}</bodyOther2>
        <bodyOther3>${form.bodyOther3.value}</bodyOther3>
    </gsmBodyDetails>
    <gsmDisplayDetails>
        <displayType>${form.displayType.value}</displayType>
        <displaySize>${form.displaySize.value}</displaySize>
        <displayResolution>${form.displayResolution.value}</displayResolution>
        <displayProtection>${form.displayProtection.value}</displayProtection>
        <displayOther1>${form.displayOther1.value}</displayOther1>
    </gsmDisplayDetails>
    <gsmMemoryDetails>
        <memoryCardSlot>${form.memoryCardSlot.value}</memoryCardSlot>
        <memoryInternal>${form.memoryInternal.value}</memoryInternal>
        <memoryOther1>${form.memoryOther1.value}</memoryOther1>
    </gsmMemoryDetails>
    <gsmSoundDetails>
        <sound35MmJack>${form.sound35MmJack.value}</sound35MmJack>
        <soundLoudspeaker>${form.soundLoudspeaker.value}</soundLoudspeaker>
        <soundOther1>${form.soundOther1.value}</soundOther1>
        <soundOther2>${form.soundOther2.value}</soundOther2>
    </gsmSoundDetails>
    <gsmBatteryDetails>
        <batteryCharging>${form.batteryCharging.value}</batteryCharging>
        <batteryType>${form.batteryType.value}</batteryType>
    </gsmBatteryDetails>
</specification>`.trim();


    try {
        const response = await fetch(`${apiUrl}/${customId}?validationType=rng`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/xml",
                "Accept": "application/xml"
            },
            body: xmlPayload
        });

        if (response.ok) {
            window.location.href = "browse.html";
        } else {
            const errText = await response.text();
            console.error(errText);
            alert("Failed to update specification.");
        }
    } catch (err) {
        console.error(err);
        alert("Error updating specification.");
    }
});

loadSpecification();
