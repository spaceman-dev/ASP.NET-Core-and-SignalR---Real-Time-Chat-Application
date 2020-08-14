var createRoomBtn = document.getElementById('create-room-btn')
var crateRoomModal = document.getElementById('create-room-modal')
createRoomBtn.addEventListener('click', function() {
    crateRoomModal.classList.add('active')
})

function closeModal() {
    crateRoomModal.classList.remove('active')
}