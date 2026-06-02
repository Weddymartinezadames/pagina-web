let display = document.getElementById("display");

function append(value) {
  if (display.textContent === "0") {
    display.textContent = value;
  } else {
    display.textContent += value;
  }
}

function clearDisplay() {
  display.textContent = "0";
}

function calculate() {
  const expression = display.textContent;

  // Usamos fetch para consumir el servicio web de MathJS
  fetch(`https://api.mathjs.org/v4/?expr=${encodeURIComponent(expression)}`)
    .then(response => response.text())
    .then(result => {
      display.textContent = result;
    })
    .catch(error => {
      display.textContent = "Error";
      console.error(error);
    });
}
