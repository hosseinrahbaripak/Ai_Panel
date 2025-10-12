document.addEventListener('DOMContentLoaded', function () {
	const fileInputs = document.querySelectorAll('.image-uploader');
	fileInputs.forEach(fileInput => {
		// Store the currently selected files in a variable that we can manipulate
		let selectedFiles = [];
		fileInput.addEventListener('change', function (event) {
			// Add new files to our selectedFiles array
			for (let i = 0; i < event.target.files.length; i++) {
				selectedFiles.push(event.target.files[i]);
			}
			let previewContainerId = fileInput.dataset.preview;
			renderPreviews(previewContainerId, fileInput, selectedFiles);
		});	
	});
	const staticPreviews = document.querySelectorAll('.image-preview-static');
	staticPreviews.forEach(preview => {
		const imagesInput = document.getElementById(preview.dataset.input);
		const removeBtns = preview.querySelectorAll('button');
		removeBtns.forEach(removeBtn => {
			removeBtn.addEventListener("click", function () {
				const containerToRemove = this.closest('.image-preview-container');
				if (containerToRemove && preview.contains(containerToRemove)) {
					preview.removeChild(containerToRemove);
				}
				if (imagesInput != null) {
					var images = imagesInput.value.split(",");
					const img = removeBtn.dataset.image;
					if (images.includes(img)) {

						const index = images.indexOf(img)
						if (index > -1) {
							images.splice(index, 1);
							imagesInput.value = images.join(',');
						}
					}
				}
			});
		});
	});

});
function renderPreviews(previewContainerId, fileInput, selectedFiles) {
	const previewContainer = document.getElementById(previewContainerId);
	previewContainer.innerHTML = '';
	if (selectedFiles.length === 0) {
		// If no files, clear the input as well (useful if all are removed)
		fileInput.value = '';
		return;
	}
	selectedFiles.forEach((file, index) => {
		if (file.type.startsWith('image/')) {
			const imgWrapper = document.createElement('div');
			const img = document.createElement('img');
			img.src = URL.createObjectURL(file); // Use createObjectURL for efficiency
			img.alt = file.name;

			const fileNameSpan = document.createElement('span');
			fileNameSpan.textContent = file.name;

			const removeButton = document.createElement('button');
			removeButton.classList.add(...["btn", "btn-danger", "rounded-circle"]);
			removeButton.innerHTML = '<i class="fa fa-close"></i>';
			removeButton.type = "button";
			removeButton.onclick = function () {
				// Revoke the object URL to free memory
				URL.revokeObjectURL(img.src);
				// Remove the file from our selectedFiles array
				selectedFiles.splice(index, 1);
				// Re-render all previews (and update the input)
				renderPreviews(previewContainerId, fileInput, selectedFiles);
			};
			imgWrapper.appendChild(img);
			imgWrapper.appendChild(fileNameSpan);
			imgWrapper.appendChild(removeButton);
			previewContainer.appendChild(imgWrapper);
		} else {
			console.warn(`File "${file.name}" is not an image and will not be previewed.`);

			selectedFiles.splice(index, 1);
		}
	});
	// Update the file input's files property with the new DataTransfer object
	// This is the key to making the actual input reflect the changes
	const dataTransfer = new DataTransfer();
	selectedFiles.forEach(file => {
		dataTransfer.items.add(file);
	});
	fileInput.files = dataTransfer.files;
}