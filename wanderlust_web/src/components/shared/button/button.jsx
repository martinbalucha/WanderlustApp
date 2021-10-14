import './Button.css';


const STYLES = ["btn--primary", "btn--outline"];
const SIZES = ["btn--medium", "btn--large"];

export const Button = ({
    children,
    type,
    handleOnClick,
    buttonStyle,
    buttonSize
}) => {
    const checkButtonStyle = STYLES.includes(buttonStyle) ? buttonStyle : STYLES[0];
    const checkButtonSize = SIZES.includes(buttonSize) ? buttonSize : SIZES[0];

    return (
        <Link to="/login" className="btn--mobile">
            <button classNam={`btn ${checkButtonStyle} ${checkButtonSize}`} onClick={handleOnClick} type={type}>
                {children}
            </button>
        </Link>
    );
};