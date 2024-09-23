import React from 'react';

const ConfirmDeleteModal = ({ isOpen, onClose, onConfirm }) => {
    if (!isOpen) return null;

    return (
        <div className="modal">
            <h2>Подтверждение удаления</h2>
            <p>Вы уверены, что хотите удалить эту книгу?</p>
            <button onClick={onConfirm}>Да</button>
            <button onClick={onClose}>Нет</button>
        </div>
    );
};

export default ConfirmDeleteModal;
