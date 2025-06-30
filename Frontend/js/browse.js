
const apiUrl = "http://localhost:5095/api/Specifications";
const token = localStorage.getItem("accessToken") || "";

async function fetchSpecifications() {
    try {
        const response = await fetch(apiUrl, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "Accept": "application/xml"
            }
        });
        const xmlText = await response.text();
        parseAndRender(xmlText);
    } catch (err) {
        console.error("Error fetching specifications:", err);
    }
}

function parseAndRender(xmlText) {
    const parser = new DOMParser();
    const xmlDoc = parser.parseFromString(xmlText, "application/xml");
    const specs = xmlDoc.getElementsByTagName("specification");

    const specList = document.getElementById("specList");
    specList.innerHTML = "";

    Array.from(specs).forEach(spec => {
        const customId = spec.getElementsByTagName("customId")[0]?.textContent ?? "";
        const phoneDetails = spec.getElementsByTagName("phoneDetails")[0];
        const yearValue = phoneDetails?.getElementsByTagName("yearValue")[0]?.textContent ?? "-";
        const brandValue = phoneDetails?.getElementsByTagName("brandValue")[0]?.textContent ?? "-";
        const modelValue = phoneDetails?.getElementsByTagName("modelValue")[0]?.textContent ?? "-";

        const detailsHtml = `
            <div class="ml-4 mt-2 space-y-1 text-sm text-gray-600 hidden details">
                <div><strong>Year:</strong> ${yearValue}</div>
                <div><strong>Launch:</strong> ${spec.getElementsByTagName("launchAnnounced")[0]?.textContent ?? "-"}</div>
                <div><strong>Status:</strong> ${spec.getElementsByTagName("launchStatus")[0]?.textContent ?? "-"}</div>
                <div><strong>Body:</strong> ${spec.getElementsByTagName("bodyDimensions")[0]?.textContent ?? "-"}, ${spec.getElementsByTagName("bodyWeight")[0]?.textContent ?? "-"}</div>
                <div><strong>Display:</strong> ${spec.getElementsByTagName("displayType")[0]?.textContent ?? "-"}, ${spec.getElementsByTagName("displaySize")[0]?.textContent ?? "-"}</div>
                <div><strong>Memory:</strong> ${spec.getElementsByTagName("memoryInternal")[0]?.textContent ?? "-"}</div>
                <div><strong>Battery:</strong> ${spec.getElementsByTagName("batteryType")[0]?.textContent ?? "-"}, ${spec.getElementsByTagName("batteryCharging")[0]?.textContent ?? "-"}</div>
            </div>
        `;

        const card = document.createElement("div");
        card.className = "bg-white shadow rounded-lg p-4 hover:bg-indigo-50 cursor-pointer transition";
        card.innerHTML = `
    <div class="flex justify-between items-center mb-2">
        <div class="text-lg font-semibold">${brandValue} ${modelValue}</div>
        <div class="text-indigo-500 text-sm">Click for details</div>
    </div>
    ${detailsHtml}
    <div class="mt-4 flex space-x-2">
        <a href="edit.html?id=${customId}"
            class="bg-blue-500 hover:bg-blue-600 text-white text-sm font-semibold py-1 px-3 rounded">
            Edit
        </a>
        <a href="editXml.html?id=${customId}"
            class="bg-blue-500 hover:bg-blue-600 text-white text-sm font-semibold py-1 px-3 rounded">
            Edit XML
        </a>
        <button class="delete-btn bg-purple-500 hover:bg-purple-600 text-white text-sm font-semibold py-1 px-3 rounded" data-id="${customId}">
            Delete
        </button>
    </div>
`;


        card.addEventListener("click", (e) => {
            if (e.target.closest("button")) return;
            const details = card.querySelector(".details");
            details.classList.toggle("hidden");
        });

        const deleteBtn = card.querySelector(".delete-btn");
        deleteBtn.addEventListener("click", async (e) => {
            e.stopPropagation();
            const specId = deleteBtn.dataset.id;
            if (!confirm(`Are you sure you want to delete specification ${specId}?`)) return;

            try {
                const response = await fetch(`http://localhost:5095/api/Specifications/${specId}`, {
                    method: "DELETE",
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });

                if (response.ok) {
                    console.log(`Deleted specification ${specId}`);
                    card.remove();
                } else {
                    console.error(`Failed to delete. Status: ${response.status}`);
                    alert("Failed to delete specification.");
                }
            } catch (err) {
                console.error("Error deleting specification:", err);
                alert("Error deleting specification.");
            }
        });

        specList.appendChild(card);
    });
}

document.addEventListener("DOMContentLoaded", () => {
  const searchInput = document.getElementById("searchInput");
  const searchBtn = document.getElementById("searchBtn");
  const specList = document.getElementById("specList");

  async function searchSpecifications(query) {
    try {
      const res = await fetch(`http://localhost:5081/api/Search?query=${encodeURIComponent(query)}`, {
        headers: {
          "Authorization": `Bearer ${token}`,
          "Accept": "application/json"
        }
      });

      if (!res.ok) throw new Error("API request failed");

      const specs = await res.json();
      renderResults(specs);
    } catch (err) {
      console.error("Error during search:", err);
      specList.innerHTML = '<p class="text-red-500">Search failed. Please try again.</p>';
    }
  }

  function renderResults(specs) {
  specList.innerHTML = "";

  if (!Array.isArray(specs) || specs.length === 0) {
    specList.innerHTML = '<p class="text-gray-500">No specifications found.</p>';
    return;
  }

  specs.forEach(spec => {
    const phone = spec.phoneDetails ?? {};
    const launch = spec.gsmLaunchDetails ?? {};
    const display = spec.gsmDisplayDetails ?? {};
    const memory = spec.gsmMemoryDetails ?? {};
    const battery = spec.gsmBatteryDetails ?? {};

    const card = document.createElement("div");
    card.className = "bg-white shadow rounded-lg p-4 mb-4";

    card.innerHTML = `
      <div class="text-lg font-semibold">${phone.brandValue ?? "-"} ${phone.modelValue ?? "-"}</div>
      <div class="text-sm text-gray-700 mt-2">
        <div><strong>Year:</strong> ${phone.yearValue ?? "-"}</div>
        <div><strong>Launch:</strong> ${launch.launchAnnounced ?? "-"}</div>
        <div><strong>Status:</strong> ${launch.launchStatus ?? "-"}</div>
        <div><strong>Display:</strong> ${display.displayType ?? "-"}, ${display.displaySize ?? "-"}</div>
        <div><strong>Resolution:</strong> ${display.displayResolution ?? "-"}</div>
        <div><strong>Memory:</strong> ${memory.memoryInternal ?? "-"}</div>
        <div><strong>Battery:</strong> ${battery.batteryType ?? "-"}, ${battery.batteryCharging ?? "-"}</div>
      </div>
    `;

    specList.appendChild(card);
  });
}

  searchBtn.addEventListener("click", () => {
    const query = searchInput.value.trim();
    if (query.length > 0) {
      searchSpecifications(query);
    }
  });
});

fetchSpecifications();

