const form = document.getElementById('articleForm');
const title = document.getElementById('title');
const content = document.getElementById('content');
const titleError = document.getElementById('titleError');
const contentError = document.getElementById('contentError');
const writerIdError = document.getElementById('writerIdError');
const successMsg = document.getElementById('successMsg');

document.addEventListener('DOMContentLoaded', function () {
    const writerDropdown = document.getElementById('writerDropdown');
        fetch('/api/Writer')
        .then(response => response.json())
        .then(data => {
            writerDropdown.innerHTML = '<option value="">Select writer</option>';
            data.forEach(writer => {
                const option = document.createElement('option');
                option.value = writer.id;
                option.textContent = writer.name;
                writerDropdown.appendChild(option);
            });
        })
        .catch(error => {
           
            writerDropdown.innerHTML = '<option value="">Failed to load writers</option>';
            
        });

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        let valid = true;
        titleError.textContent = '';
        contentError.textContent = '';
        
        successMsg.style.display = 'none';

        if (!title.value.trim()) {
            titleError.textContent = 'Title is required.';
            valid = false;
        }
        if (!content.value.trim()) {
            contentError.textContent = 'Content is required.';
            valid = false;
        }
       
        if (writerDropdown.value === "") {
            writerIdError.textContent = 'Writer is required.';
            valid = false;
        }

        if (valid) {
            const article = {
                title: title.value.trim(),
                content: content.value.trim(),
                writerId: writerDropdown.value,
                comments: []
            };

            fetch('/api/Article', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(article)
            })
                .then(response => {
                    if (response.ok) {
                        form.reset();
                        successMsg.style.display = 'block';
                        setTimeout(() => {
                            window.location.href = "index.html";
                        }, 1000);
                    } else {
                        response.text().then(msg => alert('Error: ' + msg));
                    }
                })
                .catch(error => {
                    alert('Network error: ' + error);
                });
        }

    });
});
