document.addEventListener("DOMContentLoaded", evt => {
    // Extract the current location from the URL and pass it to displayHeader and displayNavItem
    const currentLocation = window.location.pathname.split("/").pop().replace(".html", "") || "home";
    displayHeader();
    displayNavItem(currentLocation);
    displayTable();
    fetchData().catch(error => console.error("Error fetching data:", error));
});

const displayNavItem = (location) => {
    const headerItems = ["home"];
    // Dynamically generate navigation items and set the active class for the current location
    const navbar = document.querySelector("#navbar");
    if (!navbar) {
        console.error("Navbar element not found");
        return;
    }
    navbar.innerHTML = ""; // Clear existing content to avoid duplication
    headerItems.forEach((headerItem) => {
        const href = `./${headerItem === "home" ? "index" : headerItem}.html`;
        navbar.innerHTML += `
        <li class="nav-item">
            <a class="nav-link ${location === headerItem ? 'active' : ''}" aria-current="page" href="${href}">${headerItem.toUpperCase()}</a>
        </li>`;
    });
};

const displayHeader = () => {
    // Render the header with a navigation bar
    const nav = document.querySelector("#navbar");
    if (!nav) {
        console.error("Nav element not found");
        return;
    }
    nav.innerHTML = `
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Navbar</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0" id="navbar">
                </ul>
                <form class="d-flex">
                    <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                    <button class="btn btn-outline-success" type="submit">Search</button>
                </form>
            </div>
        </div>
    </nav>`;
};
const BASE_URL = "http://localhost:5096";

const fetchData = async () => {
    const endpoints = ["api/Writer", "api/Article", "api/Comment"];
    const results = [];

    for (const endpoint of endpoints) {
        try {
            const response = await fetch(`${BASE_URL}/${endpoint}`);
            if (!response.ok) {
                throw new Error(`Failed to fetch ${endpoint}: ${response.statusText}`);
            }
            
            results.push(await response.json());
            
        } catch (error) {
            console.error(`Error fetching ${endpoint}:`, error);
            results.push(null);
        }
    }

    if (results.some(r => r === null)) {
        throw new Error("One or more endpoints failed to fetch data.");
    }

    return results;
};


const displayTable = () => {
    const table = document.querySelector("#table");
    if (!table) {
        console.error("Table element not found");
        return;
    }

    table.innerHTML = `
    <table class="table table-sm table-dark">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">Nume Autor</th>
                <th scope="col">Articole</th>
                <th scope="col">E-mail Autor</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th scope="row">1</th>
                <td>Mark</td>
                <td>Otto</td>
                <td>@mdo</td>
            </tr>
            <tr>
                <th scope="row">2</th>
                <td>Jacob</td>
                <td>Thornton</td>
                <td>@fat</td>
            </tr>
            <tr>
                <th scope="row">3</th>
                <td colspan="2">Larry the Bird</td>
                <td>@twitter</td>
            </tr>
        </tbody>
    </table>`;
};
