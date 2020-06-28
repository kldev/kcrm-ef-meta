import React, { useState, useCallback } from 'react';
import { Spinner, SpinnerSize } from '@fluentui/react/lib/Spinner';
import { PrimaryButton } from '@fluentui/react/lib/Button';
import { IIconProps } from '@fluentui/react/lib/Icon';
import { delayPromise } from 'utils';

interface Props {
  iconProps?: IIconProps;
  onClick: () => Promise<void>;
}

const AppActionButton: React.FC<Props> = (props) => {
  const [showSpinner, setShowSpinner] = useState(false);

  const handlePromisedClick = useCallback(async () => {
    if (showSpinner) return;

    setShowSpinner(true);
    try {
      await delayPromise(200);
      await props.onClick();
      setShowSpinner(false);
    } catch (err) {
      setShowSpinner(false);
    }
  }, [props, showSpinner]);

  const handleOnClick = async () => {
    handlePromisedClick();
  };

  return (
    <PrimaryButton
      iconProps={showSpinner ? undefined : props.iconProps}
      onClick={handleOnClick}
    >
      {showSpinner ? <Spinner size={SpinnerSize.small} /> : props.children}
    </PrimaryButton>
  );
};

export default AppActionButton;
