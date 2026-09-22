import React from 'react';
import Modal from 'Components/Modal/Modal';
import { sizes } from 'Helpers/Props';
import CompleteSeriesInteractiveSearchModalContent, {
  CompleteSeriesInteractiveSearchModalContentProps,
} from './CompleteSeriesInteractiveSearchModalContent';

interface CompleteSeriesInteractiveSearchModalProps
  extends CompleteSeriesInteractiveSearchModalContentProps {
  isOpen: boolean;
}

function CompleteSeriesInteractiveSearchModal(
  props: CompleteSeriesInteractiveSearchModalProps
) {
  const { isOpen, seriesId, title, onModalClose } = props;

  return (
    <Modal
      isOpen={isOpen}
      size={sizes.EXTRA_EXTRA_LARGE}
      closeOnBackgroundClick={false}
      onModalClose={onModalClose}
    >
      <CompleteSeriesInteractiveSearchModalContent
        seriesId={seriesId}
        title={title}
        onModalClose={onModalClose}
      />
    </Modal>
  );
}

export default CompleteSeriesInteractiveSearchModal;
