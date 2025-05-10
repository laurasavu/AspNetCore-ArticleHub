document.addEventListener("DOMContentLoaded", async () => {
    try {
        const results = await fetchData(); 
        if (results) {
            displayArticleTitles(results);
        }
    } catch (error) {
        console.error("Error fetching data:", error);
    }
});


const fetchData = async () => {
    try {
        const response = await fetch("http://localhost:5096/api/Article/Titles");
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        let results = await response.json();
        results = Array.isArray(results) ? results : [results];
        return results;
    } catch (error) {
        console.error("Error fetching article titles:", error);
        return null;
    }
};

const displayArticleTitles = (results) => {
    if (!Array.isArray(results)) {
        console.error("Datele primite nu sunt un array:", results);
        return;
    }

    const container = document.querySelector(".container");
    const grid = container?.querySelector(".grid");

    if (!grid) {
        console.error("Grid container not found in DOM.");
        return;
    }

    results.forEach(articleTitle => {
        grid.innerHTML += `
        <div class="item">
            <h3>${articleTitle}</h3>
            <p>Short description...</p>
        </div>`;
    });
};


