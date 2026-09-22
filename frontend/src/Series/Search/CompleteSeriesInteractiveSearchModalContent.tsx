import React from 'react';
import Button from 'Components/Link/Button';
import ModalBody from 'Components/Modal/ModalBody';
import ModalContent from 'Components/Modal/ModalContent';
import ModalFooter from 'Components/Modal/ModalFooter';
import ModalHeader from 'Components/Modal/ModalHeader';
import { scrollDirections } from 'Helpers/Props';
import InteractiveSearch from 'InteractiveSearch/InteractiveSearch';
import { useClearReleasesOnUnmount } from 'InteractiveSearch/useReleases';
import translate from 'Utilities/String/translate';

export interface CompleteSeriesInteractiveSearchModalContentProps {
  seriesId: number;
  title: string;
  onModalClose(): void;
}

function CompleteSeriesInteractiveSearchModalContent({
  seriesId,
  title,
  onModalClose,
}: CompleteSeriesInteractiveSearchModalContentProps) {
  const searchPayload = {
    seriesId,
    completeSeries: true as const,
  };

  useClearReleasesOnUnmount(searchPayload);

  return (
    <ModalContent onModalClose={onModalClose}>
      <ModalHeader>
        {translate('InteractiveSearchModalHeaderCompleteSeries', { title })}
      </ModalHeader>

      <ModalBody scrollDirection={scrollDirections.BOTH}>
        <InteractiveSearch
          type="completeSeries"
          searchPayload={searchPayload}
        />
      </ModalBody>

      <ModalFooter>
        <div>{translate('CompleteSeriesSearchHelpText')}</div>
        <Button onPress={onModalClose}>{translate('Close')}</Button>
      </ModalFooter>
    </ModalContent>
  );
}

export default CompleteSeriesInteractiveSearchModalContent;
