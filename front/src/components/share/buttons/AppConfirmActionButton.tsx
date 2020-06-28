import React, { useState } from 'react';
import { Spinner, SpinnerSize } from '@fluentui/react/lib/Spinner';
import { DefaultButton, PrimaryButton } from '@fluentui/react/lib/Button';
import { IIconProps } from '@fluentui/react/lib/Icon';
import { Dialog, DialogFooter } from '@fluentui/react/lib/Dialog';

interface Props {
  iconProps?: IIconProps;
  onClick: () => Promise<void>;
  confirmText?: string;
}

const AppConfirmActionButton: React.FC<Props> = (props) => {
  const [showSpinner, setShowSpinner] = useState(false);
  const [showConfirm, setShowConfirm] = useState(false);

  const handlePromisedClick = async () => {
    if (showSpinner || showConfirm) {
      return;
    }
    setShowConfirm(true);
  };

  const handleOnConfirmed = async () => {
    setShowSpinner(true);
    setShowConfirm(false);
    try {
      await props.onClick();
      setShowSpinner(false);
    } catch (err) {
      setShowSpinner(false);
    }
  };

  const handleOnClick = () => {
    handlePromisedClick();
  };

  const handleDialogDismiss = () => {
    setShowConfirm(false);
  };

  return (
    <>
      <DefaultButton
        iconProps={showSpinner ? undefined : props.iconProps}
        onClick={handleOnClick}
      >
        {showSpinner ? <Spinner size={SpinnerSize.small} /> : props.children}{' '}
      </DefaultButton>
      {showConfirm ? (
        <Dialog onDismiss={handleDialogDismiss} hidden={false}>
          {props.confirmText ? props.confirmText : 'Are you sure?'}
          <DialogFooter>
            <PrimaryButton onClick={handleOnConfirmed} text="OK" />
            <DefaultButton onClick={handleDialogDismiss} text="Cancel" />
          </DialogFooter>
        </Dialog>
      ) : null}
    </>
  );
};

export default AppConfirmActionButton;
